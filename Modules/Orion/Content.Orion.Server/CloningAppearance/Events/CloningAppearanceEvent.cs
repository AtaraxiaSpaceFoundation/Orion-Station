using Content.Orion.Server.CloningAppearance.Components;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Player;

namespace Content.Orion.Server.CloningAppearance.Events;

public sealed class CloningAppearanceEvent : EntityEventArgs
{
    public required ICommonSession Player { get; init; }
    public required CloningAppearanceComponent Component { get; init; }
    public EntityCoordinates Coords { get; init; }
    public EntityUid? StationUid { get; init; }
    public EntityUid? MindId { get; init; }
}
