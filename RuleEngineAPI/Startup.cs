using RuleEngineAPI.Core.Builders;
using RuleEngineAPI.Core.Services;

namespace RuleEngineAPI;

public class Startup
{
    public void ConfigureServices(IServiceCollection services) 
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddAutoMapper(typeof(Startup));

        services.AddScoped<IRuleEngineService, RuleEngineService>();
        services.AddScoped<IOrganizationBenefitsBuilder, OrganizationBenefitsBuilder>();
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