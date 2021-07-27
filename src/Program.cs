using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Configuration
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var current = Directory.GetCurrentDirectory();
			var root = Directory.GetParent(current).FullName;

			var dotenv = Path.Combine(root, ".env");
			DotEnv.Load(dotenv);

			CreateHostBuilder(args).Build().Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
