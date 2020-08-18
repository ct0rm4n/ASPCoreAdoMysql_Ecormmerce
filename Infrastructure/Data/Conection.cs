using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class Conection
    {
        //string WebConfig_Con = System.Configuration.ConfigurationManager.ConnectionStrings.ToString();
        protected string connection_On = "Server=localhost; Port=3306; User Id=root;Database=challenge;Pwd=xxxv3265;";
        protected MySqlConnection connection = null;
        // GET: /Conection/
        public void Open()
        {
            try
            {


                connection = new MySqlConnection(connection_On);
                connection.Open();

            }
            catch (Exception e)
            {

                connection.Close();
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
    }
}
