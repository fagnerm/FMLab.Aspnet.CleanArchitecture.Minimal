// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

namespace FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

public class Name : IEquatable<Name>
{
    public string Value { get; init; }

    public Name(string categoryName)
    {
        this.Value = categoryName;
    }

    public bool Equals(Name? other)
    {
        return Value == other?.Value;
    }
}
