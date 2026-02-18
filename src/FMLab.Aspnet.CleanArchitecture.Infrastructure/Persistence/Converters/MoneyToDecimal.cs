// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FMLab.Aspnet.CleanArchitecture.Infrastructure.Persistence.Converters;

public class MoneyToDecimal : ValueConverter<Money, decimal>
{
    public MoneyToDecimal() :
        base(
            m => m.Value,
            m => new Money(m)
        )
    {
    }
}
