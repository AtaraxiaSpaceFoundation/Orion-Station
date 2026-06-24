using Robust.Shared.GameStates;

namespace Content.Orion.Shared.Sprint;

/// <summary>
/// Component that enables sprint functionality for mobs.
/// When sprinting, the entity gains a speed multiplier while consuming stamina.
/// After sprinting ends, a cooldown prevents immediate re-activation.
/// </summary>
[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class SprintComponent : Component
{
    /// <summary>
    /// Whether the entity is currently sprinting.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField, AutoNetworkedField]
    public bool IsSprinting;

    /// <summary>
    /// Speed multiplier applied while sprinting.
    /// For example, 2.0 means 2x normal speed.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField, AutoNetworkedField]
    public float SprintMultiplier = 2.0f;

    /// <summary>
    /// Duration of the cooldown after sprinting ends.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField, AutoNetworkedField]
    public float CooldownTime = 5.0f;

    /// <summary>
    /// Whether the entity is currently in cooldown after sprinting.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField, AutoNetworkedField]
    public bool IsOnCooldown;

    /// <summary>
    /// Remaining cooldown time. Decreases each update until reaching 0.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField, AutoNetworkedField]
    public float CooldownRemaining;

    /// <summary>
    /// Stamina required to start sprinting.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField]
    public float MinStaminaToSprint = 50f;

    /// <summary>
    /// Rate at which stamina is drained while sprinting.
    /// </summary>
    [ViewVariables(VVAccess.ReadWrite), DataField]
    public float StaminaDrainRate = 10f;
}
