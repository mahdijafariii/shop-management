using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using online_shop;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHttpContextAccessor();

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();