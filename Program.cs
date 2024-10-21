using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HACKATHON
{
	using Serilog;

	public class Program
	{
		public static void Main(string[] args)
		{
			

			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console() 
				.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // G�nl�k log dosyas� olu�turur
				.CreateLogger();

			try
			{
				Log.Information("Uygulama ba�lat�l�yor...");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Uygulama ba�lat�lamad�!");
			}
			finally
			{
				Log.CloseAndFlush();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.UseSerilog() 
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}

}
