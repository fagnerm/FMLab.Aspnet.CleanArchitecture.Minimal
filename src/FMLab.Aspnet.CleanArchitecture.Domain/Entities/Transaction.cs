// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Entities;

public class Transaction
{
    public int Id { get; private set; }
    public TransactionType Type { get; private set; }
    public string Description { get; private set; }
    public Money Amount { get; private set; }
    public DateTime CreatedAt { get; init; }

    private Transaction() { }

    public static Transaction Create(TransactionType type, Money amount)
    {
        var transaction = new Transaction()
        {
            Type = type,
            Amount = amount,
            CreatedAt = DateTime.UtcNow
        };

        return transaction;
    }

    public Transaction Update(string description)
    {
        Description = description;
        return this;
    }

    public Transaction Update(Money amount)
    {
        Amount = amount ?? Money.Zero;
        return this;
    }
}
