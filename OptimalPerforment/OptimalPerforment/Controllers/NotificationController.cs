﻿using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace OptimalPerforment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly string ConnectionString = "data source=222.252.27.138,23424\\sql2019;initial catalog=Trainer;persist security info=True;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;";
        [HttpGet]
        public async Task Get()
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
        }
    }

    public class Notification
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
