using ApplicationCore.Interfaces;
using Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class ThemaDao : Conection
    {
        public async Task<DataTable> GetThema()
        {
            Open();
            string day = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                using (var cmd = new MySqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT `ThemaId`, `NavBarFixed`, `NavBarColor`, `FontFamily`, `SideBarColor` FROM challenge.Thema";
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
        public ThemaViewModel ConvertToViewModelReadings(DataTable dataTable)
        {
            var thema = new ThemaViewModel();
            foreach (DataRow row in dataTable.Rows)
            {
                thema = new ApplicationCore.Interfaces.ThemaViewModel
                {
                    ThemaId = Convert.ToInt32(row["ThemaId"]),
                    NavBarFixed = Convert.ToBoolean(row["NavBarFixed"]),
                    NavBarColor = Convert.ToString(row["NavBarColor"]),
                    FontFamily = Convert.ToString(row["FontFamily"]),
                    SideBarColor = Convert.ToString(row["SideBarColor"])
                };
                
            }
            return thema;
        }
    }
}
