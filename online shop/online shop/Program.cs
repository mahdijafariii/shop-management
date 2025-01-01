using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using online_shop;
using online_shop.Data;
using online_shop.Middleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MongoDbContext>();
    await dbContext.ConfigureIndexesAsync();
}
startup.Configure(app, app.Environment);
app.Run();