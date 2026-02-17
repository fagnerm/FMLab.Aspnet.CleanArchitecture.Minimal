// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Entities;

public class Category
{
    public int Id { get; private set; }
    public Name Name { get; init; }

    public Category(Name name)
    {
        Name = name;
    }
}
