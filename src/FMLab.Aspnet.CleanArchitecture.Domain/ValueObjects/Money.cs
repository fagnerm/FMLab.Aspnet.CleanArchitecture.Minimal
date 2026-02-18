// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

public class Money
{
    public decimal Value { get; init; }
    public string Currency { get; init; }

    public Money(decimal amount)
    {
        Value = amount;
        Currency = "R$";
    }
}
