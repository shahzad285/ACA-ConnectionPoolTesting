// See https://aka.ms/new-console-template for more information
using ConnectionPoolTestig;
using Microsoft.Extensions.Configuration;

Console.WriteLine("Hello, World!");
var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

// Run the test with pooling enabled
Console.WriteLine("Testing database connection without pooling");
//ConnectionPool.TestConnectionWithoutPoolingAsync(connectionString);

Console.WriteLine("Testing database connection with pooling");
ConnectionPool.TestConnectionPoolingAsync(connectionString);
