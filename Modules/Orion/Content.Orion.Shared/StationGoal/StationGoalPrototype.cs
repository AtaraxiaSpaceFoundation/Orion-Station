// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Collections.Generic;
using System.IO;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Serialization.Manager.Attributes;

namespace Content.Orion.Shared.StationGoal;

[Prototype]
public sealed partial class StationGoalPrototype : IPrototype, ISerializationHooks
{
    [IdDataField]
    public string ID { get; private set; } = default!;

    [DataField]
    public string Text { get; set; } = string.Empty;

    [DataField]
    public int? MinPlayers;

    [DataField]
    public int? MaxPlayers;

    /// <summary>
    /// Goal may require certain items to complete. These items will appear near the receving fax machine at the start of the round.
    /// TODO: They should be spun up at the tradepost instead of at the fax machine, but I'm too lazy to do that right now. Maybe in the future.
    /// </summary>
    [DataField]
    public List<EntProtoId> Spawns = new();

    void ISerializationHooks.AfterDeserialization()
    {
        if (MinPlayers < 0)
            throw new InvalidDataException($"Station goal {ID} has negative minPlayers.");

        if (MaxPlayers < 0)
            throw new InvalidDataException($"Station goal {ID} has negative maxPlayers.");

        if (MinPlayers > MaxPlayers)
            throw new InvalidDataException($"Station goal {ID} has minPlayers greater than maxPlayers.");
    }
}
