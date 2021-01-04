using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace ReverseGeoCode.GrpcService
{
    /// <summary>
    /// The main class runs the app.
    /// </summary>
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Program
    {
        /// <summary>
        /// The main methods runs the app.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        // Additional configuration is required to successfully run gRPC on macOS.
        // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682
        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}