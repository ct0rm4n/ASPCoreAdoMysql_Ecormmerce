using ApplicationCore.Interfaces;
using Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repository
{
    public class CategoryDao : Conection
    {
        public void InserCategory(CategoryViewModel product)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "INSERT INTO challenge.Category" +
                "(`Name`, `Description`,`Delete`)" +
                "VALUES('" + product.Name + "', '" + product.Description + "',0);";
            Open();
            try
            {
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                //run query
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.Write(string.Format("Retorn an error ref:" + e));
            }

        }
        //post to edit product afeter isert img in ~/images/ProductName/ProductName_0.png
        public void EditCategory(CategoryViewModel product)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "UPDATE challenge.Category " +
            "SET `Name`= '" + product.Name + "',  `Description`= '" + product.Description + "',`Delete`= 0" +
            " WHERE `CategoryId`= " + product.CategoryId + ";";
            Open();
            try
            {
                MySql.Data.MySqlClient.MySqlCommand myCommand = new MySql.Data.MySqlClient.MySqlCommand(CommandText, connection);
                //run query
                myCommand.ExecuteNonQuery();
            }
            catch (MySqlException e)
            {
                Console.Write(string.Format("Retorn an error ref:" + e));
            }

        }

        public void RemoveProduct(CategoryViewModel product)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "UPDATE challenge.category " +
            "SET `Delete`=1 WHERE `CategoryId`= " + product.CategoryId + ";";
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

        public DataTable GetCategory()
        {
            Open();
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `CategoryId`, `Name`, `Description`,`Delete` FROM challenge.Category";
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

        public DataTable GetCategoryById(int Id)
        {
            Open();
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `ProductId`, `Name`,`CategoryId`, `Stock`, `Value`, `Description`,`Delete`,`Avatar` FROM challenge.product where ProductId ='" + Id + "'";
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

        public IEnumerable<object> Convert_To_ViewModel_Readings(DataTable dataTable)
        {
            //Converto datatbale to readings viewmodels in html
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new ApplicationCore.Interfaces.CategoryViewModel
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Name = Convert.ToString(row["Name"]),                    
                    Description = Convert.ToString(row["Description"]),
                    Delete = Convert.ToBoolean(row["Delete"])                    
                };
            }

        }
        public CategoryViewModel Convert_To_ViewModel(DataTable dataTable)
        {
            CategoryViewModel product = new ApplicationCore.Interfaces.CategoryViewModel();
            foreach (DataRow row in dataTable.Rows)
            {
                product = new ApplicationCore.Interfaces.CategoryViewModel
                {
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Name = Convert.ToString(row["Name"]),
                    Description = Convert.ToString(row["Description"]),
                    Delete = Convert.ToBoolean(row["Delete"])
                };
            }
            return product;

        }

    }
}
