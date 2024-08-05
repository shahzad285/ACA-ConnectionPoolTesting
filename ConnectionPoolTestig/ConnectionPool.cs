using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConnectionPoolTestig
{
    internal class ConnectionPool
    {
        public static void TestConnectionWithoutPoolingAsync(string connectionString)
        {
            int connectionCount = 1000; // Number of connections to simulate
            
            for (int i = 0; i < connectionCount; i++)
            {
                Console.WriteLine("Connection " + i);

                try
                {
                    var connection = new SqlConnection(connectionString);
                    connection.Open();
                    SqlCommand command = new SqlCommand("SELECT 1", connection);
                    command.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break; // Stop the loop when an error occurs
                }
            }

            Console.WriteLine("Test completed.");
        }

        public static void TestConnectionPoolingAsync(string connectionString)
        {
            int connectionCount = 1000; // Number of connections to simulate

            Queue<SqlConnection> queue = new Queue<SqlConnection>();

            for (int i = 0; i < 5; i++)
            {
                var connection = new SqlConnection(connectionString);
                connection.Open();
                queue.Enqueue(connection);

            }

            for (int i = 0; i < connectionCount; i++)
            {
                Console.WriteLine("Connection " + i);

                try
                {
                    var connection =queue.Dequeue();
                    SqlCommand command = new SqlCommand("SELECT 1", connection);
                    command.ExecuteScalar();
                    
                    queue.Enqueue(connection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                    break; // Stop the loop when an error occurs
                }
            }

            Console.WriteLine("Test completed.");
        }
    }
}
