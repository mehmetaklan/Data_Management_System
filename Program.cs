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
				.WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day) // Günlük log dosyasý oluþturur
				.CreateLogger();

			try
			{
				Log.Information("Uygulama baþlatýlýyor...");
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				Log.Fatal(ex, "Uygulama baþlatýlamadý!");
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
