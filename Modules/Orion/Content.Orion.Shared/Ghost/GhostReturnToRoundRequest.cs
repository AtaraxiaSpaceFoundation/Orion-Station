// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System;
using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;

namespace Content.Orion.Shared.Ghost;

[Serializable, NetSerializable]
public sealed class GhostReturnToRoundRequest : EntityEventArgs;
