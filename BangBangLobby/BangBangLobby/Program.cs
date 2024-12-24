using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký dịch vụ gRPC
builder.Services.AddGrpc();

var app = builder.Build();

// Định nghĩa endpoint cho gRPC
app.MapGrpcService<LobbyServiceImpl>();

// Endpoint test (RESTful)
app.MapGet("/", () => "Server gRPC đang chạy. Hãy dùng gRPC client để kết nối.");

app.Run();