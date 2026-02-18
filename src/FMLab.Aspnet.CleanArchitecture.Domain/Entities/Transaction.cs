// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Entities;

public class Transaction
{
    public int Id { get; set; }
    public TransactionType Type { get; set; }
    public Money Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}
