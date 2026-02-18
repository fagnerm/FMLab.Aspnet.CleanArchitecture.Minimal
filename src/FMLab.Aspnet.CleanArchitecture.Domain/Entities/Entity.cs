// API MicroSSO - Micro SSO
// Copyright (c) 2026 Fagner Marinho 
// Licensed under the MIT License. See LICENSE file in the project root for details.

using FMLab.Aspnet.CleanArchitecture.Domain.Enums;
using FMLab.Aspnet.CleanArchitecture.Domain.ValueObjects;

namespace FMLab.Aspnet.CleanArchitecture.Domain.Entities;

public class Entity
{
    public int Id { get; private set; }
    public EntityStatus Status { get; private set; }
    public Name Name { get; init; }

    private Entity() { }

    public Entity(Name name)
    {
        Name = name;
        Status = EntityStatus.Enabled;
    }

    public void Disable()
    {
        Status = EntityStatus.Disabled;
    }
}
