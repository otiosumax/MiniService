using MiniService.Middleware;
using MiniService.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IItemRepository, InMemoryItemRepository>();

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<TimingMiddleware>();
// app.MapControllers();

app.Run();