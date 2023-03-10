using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;

namespace OptimalPerforment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly string ConnectionString = "data source=222.252.27.138,23424\\sql2019;initial catalog=Trainer;persist security info=True;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;";
        [HttpGet]
        public async Task<int> Get()
        {
            string sql = "SELECT * FROM Notification";

            // Create a new SqlConnection object
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                // Open the connection
                await connection.OpenAsync();

                // Execute the query and return the results
                var data = await connection.QueryAsync<Notification>(sql);

            }

            return 1;
        }

        [HttpGet]
        public async Task<int> Insert()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Subject", typeof(string));
            dataTable.Columns.Add("Message", typeof(string));
            dataTable.Columns.Add("CreatedDate", typeof(DateTime));

            for (int i = 0; i < 1000000; i++)
            {
                dataTable.Rows.Add("Test " + i.ToString(), "abc" + i.ToString(), DateTime.Now);
            }

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = "Notification";
                    bulkCopy.BatchSize = 10000;
                    bulkCopy.BulkCopyTimeout = 3600;

                    bulkCopy.WriteToServer(dataTable);
                }
            }

            return 1;
        }
    }

    public class Notification
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }

        //public Notification(int id, string subject, string msg, DateTime date)
        //{
        //    ID = id;
        //    Subject = subject;
        //    Message = msg;
        //    CreatedDate = date;
        //}
    }
}
