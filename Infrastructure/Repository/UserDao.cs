using Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using ApplicationCore.Interfaces;
namespace Infrastructure.Repository
{
    public class UserDao : Conection
    {
        MySqlCommand comand = null;        
        public string PassGenerate(string Password)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] encodedPassword = new UTF8Encoding().GetBytes(Password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)               
               .Replace("-", string.Empty)               
               .ToLower();
            return encoded;
        }        
        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public static string CalculateMD5(string input)
        {
            string password = input;
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
            string encoded = BitConverter.ToString(hash)
               .Replace("-", string.Empty)
               .ToLower();
            return encoded;
        }
        public void InserUser(RegisterModel user)
        {
            var day = DateTime.UtcNow.ToString();
            string CommandText = "INSERT INTO challenge.User" +
                "(`Name`, `LastName`, `Email`, `Password`, `CurosId`,`Delete`,`Dt_Reg`,`Celular`,`Cpf`,`Cep`,`Rua`,`Complemento`,`Bairro`,`N`,`Es`,`Cidade`)" +
                "VALUES('" + user.UserName + "', '" + user.LastName + "','" + user.Email + "','" + PassGenerate(user.Password) + "', 1, 0,'" + day + "','" + user.Celular + "','" + user.Cpf + "','" + user.Cep + "','" + user.Rua + "','" + user.Complemento + "','" + user.Bairro + "','" + user.N + "','" + user.Es + "','" + user.Cidade + "');";
            Open();
            try
            {

                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                myCommand.ExecuteNonQuery();                
            }
            catch (MySqlException e)
            {
                Console.Write(string.Format("Retorn an error ref:" + e));
            }

        }

        public bool LoginUser(string email, string password)
        {
            string CommandText = "SELECT * FROM data_ead.User WHERE Email='" + email + "'AND Password='" + PassGenerate(password) + "'";
            Open();
            try
            {
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                MySqlDataReader return_ = myCommand.ExecuteReader();
                if (return_.Read())
                    return true;
                else return false;

            }
            catch (MySqlException e)
            {
                Close();
                Console.Write(string.Format("Retorn an error ref:" + e));
                return false;
            }

        }

        public bool VerifyifRegistered(string email)
        {
            string CommandText = "SELECT * FROM data_ead.User WHERE Email='" + email + "'";
            Open();
            try
            {
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                MySqlDataReader return_ = myCommand.ExecuteReader();                
                if (return_.Read())
                    return true;
                else return false;

            }
            catch (MySqlException e)
            {
                Close();
                Console.Write(string.Format("Retorn an error ref:" + e));
                return false;
            }
        }

        public DataTable GetUser()
        {
            Open();
            try
            {

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `UserId`, `Name`, `LastName`, `Password`, `Email`, `Cpf`, `Secret`, `CurosId`, `Dt_Reg`, `Last_LogOn`, `Avatar`, `Delete`, `Celular`, `Cep`, `Rua`, `Complemento`, `Bairro`, `N`, `Es`, `Cidade` FROM data_ead.User";
                    var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Close();
                    var dataTable = new DataTable();
                    dataTable.Load(dr);                    
                    return dataTable;

                }
            }
            catch (Exception e)
            {
                Close();
                var dataTable = new DataTable();                
                return dataTable;
            }
        }


    }
}