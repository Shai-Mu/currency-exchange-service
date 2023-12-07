using CurrencyExchangeService.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyExchangeService.Database.Context.Configurations;

public class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.HasIndex(a => new { a.UserId, a.CurrencyId })
            .IsUnique();
        builder.HasKey(a => new { a.UserId, a.CurrencyId });

        builder.HasOne(a => a.Currency)
            .WithMany()
            .HasForeignKey(a => a.CurrencyId);

        builder.Property(a => a.Balance)
            .IsRequired();
    }
}