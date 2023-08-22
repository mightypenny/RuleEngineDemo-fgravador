using EmployeeService.Core.Services;
using EmployeeService.Repositories;
using EmployeeService.Repositories.MongoDB;
using EmployeeService.Repositories.Postgres;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services) 
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddAutoMapper(typeof(Startup));
        
        services.AddScoped<IEmployeeService, Core.Services.EmployeeService>();
        services.AddScoped<IEmployeeRepository, MongoDBEmployeeRepository>();
        services.AddScoped<IEmployeeRepository, PostgresDBEmployeeRepository>();
        
        services.AddDbContext<EmployeeDBContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("EmployeePostgres")));
        
        services.Configure<AppSettings>(configuration.GetSection("AppSettings"));
    }
    
    public void Configure(WebApplication app, IWebHostEnvironment env) 
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
    }
}