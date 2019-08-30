using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using Docker.DotNet;
using Docker.DotNet.Models;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Build connection string
                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.DataSource = "localhost,11433";   // update me
                builder.UserID = "SA";              // update me
                builder.Password = "Teste!@#$";      // update me
                builder.InitialCatalog = "master";

                // Connect to SQL
                Console.Write("Connecting to SQL Server ... ");
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    Console.WriteLine("Done.");
                }


                

            }
            catch (SqlException e)
            {
                Console.WriteLine("Inicializando Docker...");
                new DockerConnect().Connect();


            }

            Console.WriteLine("All done. Press any key to finish...");
            Console.ReadKey(true);
        }


    }

    public class DockerConnect
    {
        public async void Connect()
        {

            using (DockerClient client = new DockerClientConfiguration(new Uri("http://localhost:2375")).CreateClient())
            {
                IList<ContainerListResponse> containers = await client.Containers.ListContainersAsync(
                    new ContainersListParameters()
                    {
                        Limit = 10,
                    });

                await client.Containers.StartContainerAsync("d949756de75f",
                    new ContainerStartParameters()
                    );

            }
        }
    }
}
