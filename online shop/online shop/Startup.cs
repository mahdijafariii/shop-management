using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using online_shop.Data;
using online_shop.Middleware;
using online_shop.Repositories;
using online_shop.Repositories.CategoryRepository;
using online_shop.Repositories.SellerRepository;
using online_shop.Services;
using online_shop.Services.CategoryService;
using online_shop.Services.SellerService;
using online_shop.Validator;
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
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                    ClockSkew = TimeSpan.Zero 
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var token = context.Request.Cookies["Access-cookie"];
                        if (!string.IsNullOrEmpty(token))
                        {
                            context.Token = token;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        
        

        // redis configuration
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = Configuration.GetConnectionString("RedisConnection");
        });
        var redisConnection = Configuration.GetConnectionString("RedisConnection");
        services.AddScoped<MongoDbContext>();
        services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnection));
        services.AddScoped<IDatabase>(sp => sp.GetRequiredService<IConnectionMultiplexer>().GetDatabase());

 
        
        services.AddControllers()
            .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<AddAddressValidator>());
        
        
        services.AddScoped<IBanUsersRepository, BanUsersRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISellerRepository, SellerRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        
        
        
        
        services.AddScoped<IBanService,BanService>();
        services.AddScoped<IJwtService,JwtService>();
        services.AddScoped<IOtpService,OtpService>();
        services.AddScoped<ISmsService,SmsService>();
        services.AddScoped<ICookieService,CookieService>();
        services.AddScoped<IAuthService,AuthService>();
        services.AddScoped<IAdminService,AdminService>();
        services.AddScoped<IUserService,UserService>();
        services.AddScoped<ISellerService,SellerService>();
        services.AddScoped<ICategoryService,CategoryService>();



    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();       

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers(); 
        });
    }
}