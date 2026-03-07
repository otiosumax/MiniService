using MiniService.Middleware;
using MiniService.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IItemRepository, InMemoryItemRepository>();
builder.Services.AddControllers();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TimingMiddleware>();
app.MapControllers();

app.Run();