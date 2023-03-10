using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Concurrent;
using System.Data.SqlClient;

namespace OptimalPerforment.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParallelController : ControllerBase
    {
        public string connectionString = "data source=222.252.27.138,23424\\sql2019;initial catalog=Trainer;persist security info=True;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;";

        

        [HttpGet]
        public async Task<int> Main()
        {
            string sqlQuery = "SELECT * FROM Notification";

            //ConcurrentBag<Notification> rowData = new ConcurrentBag<Notification>();

            //int batchSize = 5000;
            //int totalRows = 1000000;
            //int iterations = totalRows / batchSize;

            //ParallelOptions options = new ParallelOptions();
            //options.MaxDegreeOfParallelism = Environment.ProcessorCount;

            //Parallel.For(0, iterations, options, i =>
            //{
            //    int offset = i * batchSize;
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();
            //        using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            //        {
            //            command.CommandType = System.Data.CommandType.Text;
            //            command.CommandText = $"{sqlQuery} ORDER BY ID OFFSET {offset} ROWS FETCH NEXT {batchSize} ROWS ONLY";
            //            using (SqlDataReader reader = command.ExecuteReader())
            //            {
            //                while (reader.Read())
            //                {
            //                    rowData.Add(new Notification(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3)));
            //                }
            //            }
            //        }
            //    }
            //});

            //ConcurrentQueue<Notification> rowData = new ConcurrentQueue<Notification>();

            //int batchSize = 200000;
            //int totalRows = 1000000;
            //int iterations = totalRows / batchSize;

            //List<Task> tasks = new List<Task>();

            //for (int i = 0; i < iterations; i++)
            //{
            //    int offset = i * batchSize;
            //    int localBatchSize = batchSize;

            //    if (i == iterations - 1)
            //    {
            //        // Last batch might be smaller
            //        localBatchSize = totalRows - offset;
            //    }

            //    tasks.Add(Task.Run(async () =>
            //    {
            //        using (SqlConnection connection = new SqlConnection(connectionString))
            //        {
            //            await connection.OpenAsync();
            //            using (SqlCommand command = new SqlCommand(sqlQuery, connection))
            //            {
            //                command.CommandType = System.Data.CommandType.Text;
            //                command.CommandText = $"{sqlQuery} ORDER BY ID OFFSET {offset} ROWS FETCH NEXT {localBatchSize} ROWS ONLY";
            //                using (SqlDataReader reader = await command.ExecuteReaderAsync())
            //                {
            //                    while (await reader.ReadAsync())
            //                    {
            //                        rowData.Enqueue(new Notification(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetDateTime(3)));
            //                    }
            //                }
            //            }
            //        }
            //    }));
            //}

            //await Task.WhenAll(tasks);

            return 1;
        }

        [HttpGet]
        public async Task<int> LinqGet()
        {
            using (var db = new MyDbContext())
            {
                var query = db.Notification.AsParallelOrdered(c => c.ID, Comparer<int>.Default);
                var results = await Task.Run(() => query.ToList());

                //foreach (var customer in results)
                //{
                //    // process the customer here
                //}
            }

            return 1;
        }

        public class MyDbContext : DbContext
        {
            public DbSet<Notification> Notification { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlServer("data source=222.252.27.138,23424\\sql2019;initial catalog=Trainer;persist security info=True;user id=sa;password=247@dev2021;multipleactiveresultsets=True;Max Pool Size=1000;Connect Timeout=600;Encrypt=True;TrustServerCertificate=True");
            }
        }
    }

    public static class ParallelExtensions
    {
        public static IEnumerable<TResult> AsParallelOrdered<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, IComparer<TResult> comparer)
        {
            return (IEnumerable<TResult>)source.AsParallel().OrderBy(selector, comparer);
        }
    }
}
