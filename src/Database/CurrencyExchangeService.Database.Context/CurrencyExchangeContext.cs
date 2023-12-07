using CurrencyExchangeService.Database.Context.Configurations;
using CurrencyExchangeService.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace CurrencyExchangeService.Database.Context;

public class CurrencyExchangeContext : DbContext
{
    #nullable disable
    public DbSet<User> Users { get; set; }
    
    public DbSet<Currency> Currencies { get; set; }
    
    public DbSet<Account> Accounts { get; set; }
    #nullable restore

    public CurrencyExchangeContext(DbContextOptions<CurrencyExchangeContext> options)
        : base(options)
    {
        
    }

    public CurrencyExchangeContext()
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new AccountConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
}