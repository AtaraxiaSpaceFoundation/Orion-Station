// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Robust.Shared.Configuration;

namespace Content.Orion.Shared.CCVar;

public sealed partial class OrionCCVars
{
    /*
     * Ghost Respawn
     */

    public static readonly CVarDef<bool> GhostRespawnEnabled =
        CVarDef.Create("ghost.respawn_enabled", true, CVar.REPLICATED | CVar.SERVER);

    public static readonly CVarDef<float> GhostRespawnTime =
        CVarDef.Create("ghost.respawn_time", 300f, CVar.REPLICATED | CVar.SERVER);

    public static readonly CVarDef<int> GhostRespawnMaxPlayers =
        CVarDef.Create("ghost.respawn_max_players", 80, CVar.SERVERONLY);

    public static readonly CVarDef<bool> GhostRespawnCheckSameCharacter =
        CVarDef.Create("ghost.respawn_check_same_character", true, CVar.ARCHIVE | CVar.SERVERONLY);
}
