using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BangBangLobby
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.ListenLocalhost(5000, listenOptions =>
                            {
                                listenOptions.UseHttps();  // Nếu muốn sử dụng HTTPS
                            });
                        })
                        .UseStartup<Startup>();  // Chỉ định lớp Startup
                });
    }
}