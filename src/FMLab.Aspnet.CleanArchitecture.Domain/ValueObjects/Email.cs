// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Extensions;
using System.Net.Mail;

namespace FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

public class Email
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
}
