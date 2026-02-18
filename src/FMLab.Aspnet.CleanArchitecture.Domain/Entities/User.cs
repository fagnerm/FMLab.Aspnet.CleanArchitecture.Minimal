// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Users;

public class User
{
    public int Id { get; private set; }
    public UserStatus Status { get; private set; }
    public Name Name { get; init; }

    private User() { }

    public User(Name name)
    {
        Name = name;
        Status = UserStatus.Enabled;
    }

    public void Disable()
    {
        Status = UserStatus.Disabled;
    }
}
