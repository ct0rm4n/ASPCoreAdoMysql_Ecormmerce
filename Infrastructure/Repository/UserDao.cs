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
        // GET: /UserDao/
        //CRIPTOGRAFIA - gerador de senha
        public string MD5Generate(string Password)
        {
            MD5 md5Hasher = MD5.Create();
            byte[] encodedPassword = new UTF8Encoding().GetBytes(Password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();
            return encoded;
        }
        //
        // GET: /Account/LogOn
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
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
            // given, a password in a string
            string password = input;

            // byte array representation of that string
            byte[] encodedPassword = new UTF8Encoding().GetBytes(password);

            // need MD5 to calculate the hash
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);

            // string representation (similar to UNIX format)
            string encoded = BitConverter.ToString(hash)
               // without dashes
               .Replace("-", string.Empty)
               // make lowercase
               .ToLower();

            return encoded;
        }
        public void InserUser(RegisterModel user)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "INSERT INTO data_ead.User" +
                "(`Name`, `LastName`, `Email`, `Password`, `CurosId`,`Delete`,`Dt_Reg`,`Celular`,`Cpf`,`Cep`,`Rua`,`Complemento`,`Bairro`,`N`,`Es`,`Cidade`)" +
                "VALUES('" + user.UserName + "', '" + user.LastName + "','" + user.Email + "','" + MD5Generate(user.Password) + "', 1, 0,'" + day + "','" + user.Celular + "','" + user.Cpf + "','" + user.Cep + "','" + user.Rua + "','" + user.Complemento + "','" + user.Bairro + "','" + user.N + "','" + user.Es + "','" + user.Cidade + "');";
            Open();
            try
            {

                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                //run query
                myCommand.ExecuteNonQuery();
                //myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (MySqlException e)
            {
                Console.Write(string.Format("Retorn an error ref:" + e));
            }

        }

        public bool Login_User(string email, string password)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");

            string CommandText = "SELECT * FROM data_ead.User WHERE Email='" + email + "'AND Password='" + MD5Generate(password) + "'";
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

        public bool Verify_if_Registered(string email)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "SELECT * FROM data_ead.User WHERE Email='" + email + "'";
            Open();
            try
            {

                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                //run query
                MySqlDataReader return_ = myCommand.ExecuteReader();
                //var resul_query = myCommand.ExecuteReader(CommandBehavior.CloseConnection).RecordsAffected;

                //var result =false;
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

            string day = DateTime.Now.ToString("yyyy-MM-dd");

            try
            {

                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `UserId`, `Name`, `LastName`, `Password`, `Email`, `Cpf`, `Secret`, `CurosId`, `Dt_Reg`, `Last_LogOn`, `Avatar`, `Delete`, `Celular`, `Cep`, `Rua`, `Complemento`, `Bairro`, `N`, `Es`, `Cidade` FROM data_ead.User";

                    var dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    Close();
                    //var dataReader = cmd.ExecuteReader();
                    var dataTable = new DataTable();
                    dataTable.Load(dr);
                    //UserViewBag = dataTable;
                    return dataTable;

                }
            }
            catch (Exception e)
            {
                Close();
                var dataTable = new DataTable();
                //UserViewBag = dataTable;
                return dataTable;
            }
        }


    }
}