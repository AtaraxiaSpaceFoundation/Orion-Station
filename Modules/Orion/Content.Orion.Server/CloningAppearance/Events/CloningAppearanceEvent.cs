// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Orion.Server.CloningAppearance.Components;
using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Player;

namespace Content.Orion.Server.CloningAppearance.Events;

public sealed class CloningAppearanceEvent : EntityEventArgs
{
    public ICommonSession Player = default!;
    public CloningAppearanceComponent Component = default!;
    public EntityCoordinates Coords { get; set; }
    public EntityUid? StationUid { get; set; }
    public EntityUid? MindId { get; set; }
}
