// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Users;

public class User
{
    public int Id { get; private set; }
    public Name Name { get; init; }
    public Email EMail { get; init; }
    public UserStatus Status { get; private set; }

    private User()
    {
        Name = null!;
        EMail = null!;
    }

    public User(Name name, Email email)
    {
        Name = name;
        Status = UserStatus.Active;
        EMail = email;
    }

    public void Deactivate()
    {
        Status = UserStatus.Deactivated;
    }
}
