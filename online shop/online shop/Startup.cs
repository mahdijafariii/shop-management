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