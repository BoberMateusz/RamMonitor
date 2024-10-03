using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace RamMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            int operationLimit = Convert.ToInt32(ConfigurationManager.AppSettings["operationLimit"]);
            int monitorDelay = Convert.ToInt32(ConfigurationManager.AppSettings["monitorDelay"]);
            Console.WriteLine($"Monitoring RAM every {monitorDelay}ms, will stop after {operationLimit} iterations");
            for (int i = 0; i < operationLimit; i++)
            {
                var memoryUsage = getTotalMemoryUsage();
                var currentTime = DateTime.Now;

                WriteToDatabase(memoryUsage, currentTime);
                Console.WriteLine($"Inserted: {memoryUsage}, {currentTime}");
                Thread.Sleep(monitorDelay);
            }
        }

        private static double getTotalMemoryUsage()
        {
            var processes = Process.GetProcesses();
            return processes.Sum(p => p.WorkingSet64);
        }

        private static void WriteToDatabase(double memoryUsage, DateTime currentTime)
        {
            var connectionString = ConfigurationManager.AppSettings["connectionString"];
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand("INSERT INTO MemoryUsageData (MemoryUsage, Time) " +
                   "VALUES (@MemoryUsage, @Time)", connection);

                command.Parameters.AddWithValue("@Time", currentTime);
                command.Parameters.AddWithValue("@MemoryUsage", memoryUsage);
                command.ExecuteNonQuery();
            }
        }
    }
}
