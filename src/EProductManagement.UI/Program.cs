using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.IO;

namespace EProductManagement.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DateTime today = DateTime.Now;

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Logger(lg1 => lg1
                    //.Filter.ByIncludingOnly(x => x.Properties.ContainsKey("TemperatureC"))
                    .WriteTo.MongoDB(configuration["MongoDBSetting:ConnectionString"], collectionName: today.Year + "_" + today.Month + "_" + today.Day)
                )
                //.WriteTo.Logger(lg2 => lg2
                //    //.Filter.ByIncludingOnly(x => x.Properties.ContainsKey("TemperatureK"))
                //    //.Filter.ByIncludingOnly(Matching.WithProperty("TemperatureK"))
                //    .Filter.ByIncludingOnly(Matching.WithProperty("kelvin"))
                //    .WriteTo.MongoDB("mongodb://localhost/mongotest", collectionName: "testlog2")
                //)
                //.MongoDB("mongodb://localhost/mongotest", collectionName: "testlog1")
                .Enrich.FromLogContext()
                ////.Enrich.
                .CreateLogger();

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .UseSerilog();
    }
}
