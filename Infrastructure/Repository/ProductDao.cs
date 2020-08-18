using ApplicationCore.Interfaces;
using Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repository
{
    public class ProductDao : Conection
    {
        public void InserProduct(ProductViewModel product)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "INSERT INTO challenge.Product" +
                "(`Name`, `CategoryId`, `Estoque`,  `Description`,`Value`,`Delete`,`Avatar`)" +
                "VALUES('" + product.Name + "', '" + product.CategoryId + "', '" + product.Estoque + "', '"  + product.Description + "','" + product.Value + "', 0,'"+product.Avatar+"');";
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
        public void EditProduct(ProductViewModel product)
        {
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            string CommandText = "UPDATE challenge.product "+
            "SET `Name`= '"+product.Name+ "', `CategoryId`= " + product.CategoryId + "`Estoque`= " + product.Estoque + ", `Description`= '" + product.Description + "', `Value`= "+product.Value+", `Delete`= 0, `Avatar`= '"+product.Avatar+"'" +
            " WHERE `ProductId`= " + product.ProductId + ";";
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
        //return all products
        public DataTable GetProducts()
        {
            //LIST ALL product of database to a datatable
            Open();
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `ProductId`, `Name`, `CategoryId`, `Estoque`, `Value`, `Description`,`Delete`,`Avatar`  FROM challenge.Product";
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
        //GetProductBy id - Used in Backend Grid
        public DataTable GetProductById(int Id)
        {
            //LIST ALL product of database to a datatable
            Open();
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `ProductId`, `Name`,`CategoryId`, `Estoque`, `Value`, `Description`,`Delete`,`Avatar` FROM challenge.product where ProductId ='" + Id+"'";
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
        //Convert mult rows and single record to viewbag 
        public IEnumerable<object> Convert_To_ViewModel_Readings(DataTable dataTable)
        {
            //Converto datatbale to readings viewmodels in html
            foreach (DataRow row in dataTable.Rows)
            {
                yield return new ApplicationCore.Interfaces.ProductViewModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Name = Convert.ToString(row["Name"]) ,
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Estoque = Convert.ToInt32(row["Estoque"]),
                    Value = Convert.ToDouble(row["Value"]),
                    Description = Convert.ToString(row["Description"]),
                    Delete = Convert.ToBoolean(row["Delete"]),
                    Avatar = Convert.ToString(row["Avatar"])??"N/A"
                };
            }

        }
        public ProductViewModel Convert_To_ViewModel(DataTable dataTable)
        {
            //Converto datatbale to readings viewmodels in html
            ProductViewModel product = new ApplicationCore.Interfaces.ProductViewModel();
            foreach (DataRow row in dataTable.Rows)
            {
                //foreach but datatable count have a 1 rows
                product = new ApplicationCore.Interfaces.ProductViewModel
                {
                    ProductId = Convert.ToInt32(row["ProductId"]),
                    Name = Convert.ToString(row["Name"]),
                    CategoryId = Convert.ToInt32(row["CategoryId"]),
                    Estoque = Convert.ToInt32(row["Estoque"]),
                    Value = Convert.ToDouble(row["Value"]),
                    Description = Convert.ToString(row["Description"]),
                    Delete = Convert.ToBoolean(row["Delete"]),
                    Avatar = Convert.ToString(row["Avatar"]) ?? "N/A"
                };

            }
            return product;

        }

    }
}
