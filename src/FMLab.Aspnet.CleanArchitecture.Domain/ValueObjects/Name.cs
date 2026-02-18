// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Extensions;
using System.Text.RegularExpressions;

namespace FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

public class Name : IEquatable<Name>
{
    public string Value { get; init; }

    public Name(string name)
    {
        name.ThrowIfNullOrEmpty("Must inform a name");
        if (!IsValid(name))
        {
            DomainException.Throw("Must inform a valid name");
        }

        Value = name;
    }

    public bool Equals(Name? other)
    {
        return Value == other?.Value;
    }

    private bool IsValid(string name)
    {
        var regex = @"/^[A-Za-z0-9]+$/";
        return new Regex(regex).IsMatch(name);

    }
}
