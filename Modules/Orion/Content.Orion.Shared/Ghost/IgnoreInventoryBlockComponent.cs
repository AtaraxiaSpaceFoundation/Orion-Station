using Robust.Shared.Analyzers;
using Robust.Shared.GameObjects;
using Robust.Shared.GameStates;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Shared.Ghost;

[RegisterComponent, AutoGenerateComponentState, NetworkedComponent]
public sealed partial class IgnoreInventoryBlockComponent : Component
{
    /// <summary>
    ///      If true, the entity can interact with blocked inventory slots.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool IgnoreBlock = true;

    /// <summary>
    ///     If true, the entity can see all items including those in hidden slots.
    /// </summary>
    [DataField, AutoNetworkedField]
    public bool ShowAllItems = true;
}
