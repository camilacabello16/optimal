using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Data.Sqlite;

namespace OptimalPerforment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperController : ControllerBase
    {
        private readonly string ConnectionString = "data source=222.252.27.138,23424\\sql2019;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;";

        [HttpGet]
        public async Task<int> Get()
        {
            using (var connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = "SELECT * FROM Notification";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var data = reader;

                    }
                }
            }

            return 1;
        }
    }
}
