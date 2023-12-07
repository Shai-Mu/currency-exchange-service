using CurrencyExchangeService.Database.Context;
using CurrencyExchangeService.RestApi;
using CurrencyExchangeService.RestApi.Extensions;

public static class Program
{
    public static void Main(string[] args)
    {
        try
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<CurrencyExchangeContext>()
                .Run();
        }
        catch (Exception e)
        {
            Console.WriteLine(e); // Serilog
        }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults((webBuilder) =>
            {
                webBuilder.UseStartup<Startup>();
            });
}