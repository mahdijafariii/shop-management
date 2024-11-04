using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using online_shop;

var builder = WebApplication.CreateBuilder(args);

// استفاده از Startup برای پیکربندی
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
startup.Configure(app, app.Environment);

app.Run();