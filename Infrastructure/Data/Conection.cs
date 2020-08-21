using Infrastructure.Repository;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Data
{
    public class Conection
    {
        public string connection_On = "Server=localhost; Port=3306; User Id=root;Pwd=xxxv3265; Database=challenge";
        public string connection_On_HangFire = "server=localhost; database=HangFire; password='xxxv3265'; uid=root; port=3306; Allow User Variables=True";
        protected MySqlConnection connection = null;
        protected string database = "create DATABASE if not exists `HangFire`;"+
                                    "create DATABASE if not exists `challenge`;" +
                                    "use `challenge`;" +
                                    "CREATE TABLE if not exists `Category` (" +
                                        "`CategoryId` INT NOT NULL AUTO_INCREMENT," +
                                        "`Name` VARCHAR(240) NOT NULL," +
                                        "`Description` VARCHAR(240) NOT NULL," +
                                        "`Delete` BOOLEAN DEFAULT '0'," +
                                        "PRIMARY KEY(`CategoryId`)" +
                                    ");" +
                                    "CREATE TABLE if not exists `Product` (" +
                                        "`ProductId` INT NOT NULL AUTO_INCREMENT," +
                                        "`Name` VARCHAR(240) NOT NULL," +
                                        "`Description` VARCHAR(240) NOT NULL," +
                                        "`Value` DECIMAL NOT NULL," +
                                        "`CategoryId` INT NOT NULL," +
                                        "`Stock` INT DEFAULT '0'," +
                                        "`Avatar` VARCHAR(240)," +
                                        "`Delete` BOOLEAN DEFAULT '0'," +
                                        "UNIQUE KEY `ProductCategory` (`ProductId`,`CategoryId`) USING BTREE," +
                                        "PRIMARY KEY(`ProductId`)" +
                                    ");";
                                    


        public void Open()
        {
            try
            {   
                
                connection = new MySqlConnection(connection_On);                
                connection.Open();
                

            }
            catch (Exception e)
            {
                MySqlConnection connection_db = new MySqlConnection("Server=localhost; Port=3306; User Id=root;Pwd=xxxv3265;");
                MySql.Data.MySqlClient.MySqlCommand myCommandCreateDb = new MySql.Data.MySqlClient.MySqlCommand(database, connection_db);
                connection.Open();
                myCommandCreateDb.ExecuteNonQuery();
                
                connection.Close();
                Open();
                throw e;

            }
            finally
            {

            }
        }

        public void Close()
        {
            try
            {
                connection = new MySqlConnection(connection_On);
                connection.Close();

            }
            catch (Exception e)
            {

                connection.Close();
                throw e;

            }
            finally
            {
                connection.Close();
            }
        }



        public void CreateDatabase() { 
            
        }

    }
}
