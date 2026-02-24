// API - Clean architecture boilerplate
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Extensions;
using System.Net.Mail;

namespace FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

public class Email : IEquatable<Email>
{
    public string Value { get; private set; }

    public Email(string email)
    {
        if (string.IsNullOrEmpty(email) || !IsValidEmail(email))
        {
            DomainGuard.Throw("Invalid email format");
        }

        Value = email;
    }

    private bool IsValidEmail(string value)
    {
        try
        {
            var address = new MailAddress(value);
            return address.Address == value;
        }

        catch
        {
            return false;

        }
    }

    public bool Equals(Email? other)
    {
        return Value == other?.Value;
    }

    public int CompareTo(Email? other)
    {
        return string.Compare(Value, other?.Value, StringComparison.Ordinal);
    }

    public static bool operator ==(Email left, Email right)
    {
        if (left is null) return right is null;
        return left.Value.Equals(right?.Value);
    }

    public static bool operator !=(Email left, Email right) => !(left == right);

    public override int GetHashCode() => Value.GetHashCode();

    public override string ToString() => Value.ToString();
}
