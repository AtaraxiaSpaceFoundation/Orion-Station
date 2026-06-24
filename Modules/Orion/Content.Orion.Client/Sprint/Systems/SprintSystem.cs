using Content.Shared.Input;
using Robust.Client.Input;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;

namespace Content.Orion.Client.Sprint.Systems;

/// <summary>
/// Client-side sprint system that registers the Space key binding for sprint.
/// The input handler itself lives in SharedSprintSystem via CommandBinds.
/// </summary>
public sealed partial class ClientSprintSystem : EntitySystem
{
    [Dependency] private IInputManager _inputManager = default!;

    public override void Initialize()
    {
        base.Initialize();

        // Register Space key binding for Sprint (State type: hold to sprint, release to stop)
        _inputManager.RegisterBinding(new KeyBindingRegistration
        {
            Function = ContentKeyFunctions.Sprint,
            BaseKey = Keyboard.Key.Space,
            Type = KeyBindingType.State
        });
    }
}
