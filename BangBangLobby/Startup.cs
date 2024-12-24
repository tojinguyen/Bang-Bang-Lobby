using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BangBangLobby
{
    public class Startup
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();  // Thêm gRPC service vào DI container
        }

        public static void Configure(IApplicationBuilder app, IHostEnvironment env)
        {
            app.UseRouting();

            // Đăng ký dịch vụ gRPC
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<LobbyServiceImpl>();  // Map service của chúng ta
            });
        }
    }
}