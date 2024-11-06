using online_shop.Data;
using online_shop.Repositories;
using online_shop.Services;
using StackExchange.Redis;

namespace online_shop;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); 
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();


        // redis configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("RedisConnection");
        });
        var redisConnection = Configuration.GetConnectionString("RedisConnection");
        Console.WriteLine(redisConnection);
        services.AddScoped<MongoDbContext>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
        services.AddScoped<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

        
        
        services.AddScoped<IBanUsersRepository, BanUsersRepository>();
        
        
        
        
        services.AddScoped<IBanService,BanService>();
        services.AddScoped<IJwtService,JwtService>();
        services.AddScoped<IOtpService,OtpService>();
        services.AddScoped<ISmsService,SmsService>();



    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseRouting(); 

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); 
        });
    }
}