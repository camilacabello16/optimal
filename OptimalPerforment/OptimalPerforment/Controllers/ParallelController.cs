using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace OptimalPerforment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParallelController : ControllerBase
    {
        [HttpGet]
        public void Main()
        {
            string connectionString = "data source=222.252.27.138,23424\\sql2019;initial catalog=Trainer;persist security info=True;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;";
            string sqlQuery = "SELECT * FROM Notification";
            int chunkSize = 10000;

            List<Task> tasks = new List<Task>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlQuery, connection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int chunkIndex = reader.RecordsAffected / chunkSize;
                    if (tasks.Count <= chunkIndex)
                    {
                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            for (int i = 0; i < chunkSize; i++)
                            {
                                if (reader.Read())
                                {
                                    // process the data, for example, write it to a file or display it on the screen
                                    var ID = reader.GetInt64(0);

                                }
                                else
                                {
                                    break;
                                }
                            }
                        }));
                    }
                }

                Task.WaitAll(tasks.ToArray());
            }
        }

        //private void ProcessChunk(SqlDataReader reader, int chunkSize)
        //{
            
        //}
    }
}
