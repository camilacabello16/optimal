using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Xml.Linq;
using System.Diagnostics;

namespace OptimalPerforment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperController : ControllerBase
    {
        private readonly string ConnectionString = "data source=222.252.27.138,23424\\sql2019;initial catalog=Trainer;persist security info=True;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;";

        [HttpGet]
        public async Task<int> Get()
        {
            //SqlConnection connection = new SqlConnection(ConnectionString);
            //string sql = "SELECT * FROM Notification";
            //SqlCommand command = new SqlCommand(sql, connection);

            //connection.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //var listData = new List<Notification>();
            //while (reader.Read())
            //{
            //    // retrieve data from the reader
            //    var data = new Notification
            //    {
            //        ID = reader.GetInt32(0),
            //        Subject = reader.GetString(1),
            //        Message = reader.GetString(2),
            //        CreatedDate = reader.GetDateTime(3),
            //    };
            //    listData.Add(data);

            //    // process the data
            //    // ...
            //}
            //reader.Close();
            //connection.Close();

            return 1;
        }
    }
}
