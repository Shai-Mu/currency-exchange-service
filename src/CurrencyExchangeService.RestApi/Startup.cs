using CurrencyExchangeService.Core.Interfaces;
using CurrencyExchangeService.Database.Context;
using CurrencyExchangeService.Database.Repositories;
using CurrencyExchangeService.ExchangeCalculator;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace CurrencyExchangeService.RestApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
    
    public IConfiguration Configuration { get; }
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers()
            .AddNewtonsoftJson();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange service", Version = "v1" });
            
        });
        services.AddSwaggerGenNewtonsoftSupport();
        
        services.AddDbContext<CurrencyExchangeContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("Default"));
        });

        services.AddScoped<ICurrencyRepository, CurrencyRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<IUserOperationsFacade, UserOperationsFacade.UserOperationsFacade>();
        services.AddScoped<ICurrencyOperationsFacade, CurrencyOperationsFacade.CurrencyOperationsFacade>();
        services.AddScoped<IExchangeService, ExchangeService>();
        services.AddScoped<IDatabaseCommitter, DatabaseCommitter>();

        services.AddControllers()
            .AddNewtonsoftJson();
    }
    
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OpenAPI definition v1"));

        app.UseRouting();
        app.UseCors("policy"); // Must be after Routing and before Authorization middlewares.

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}