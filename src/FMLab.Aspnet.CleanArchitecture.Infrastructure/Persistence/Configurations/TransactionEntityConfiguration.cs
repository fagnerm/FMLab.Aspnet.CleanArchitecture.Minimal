// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Entities;
using FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Configurations;

public class TransactionEntityConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder
            .HasKey(t => t.Id);
        builder
            .Property(t => t.Type)
            .HasConversion<string>()
            .IsRequired();
        builder
            .Property(t => t.Amount)
            .HasConversion<MoneyToDecimal>()
            .IsRequired();
        builder
            .Property(t => t.CreatedAt)
            .IsRequired();
    }
}
