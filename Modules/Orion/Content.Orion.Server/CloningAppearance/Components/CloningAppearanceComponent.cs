using Content.Shared.Roles;
using Robust.Shared.GameObjects;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Server.CloningAppearance.Components;

[RegisterComponent]
public sealed partial class CloningAppearanceComponent : Component
{
    [DataField]
    [AlwaysPushInheritance]
    public ComponentRegistry Components { get; private set; } = new();

    [DataField]
    public ProtoId<StartingGearPrototype>? StartingGear;

    [DataField]
    public bool CopyTraits;
}
