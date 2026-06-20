// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Construction.Prototypes;
using Content.Shared.Preferences;
using Robust.Shared.Prototypes;

namespace Content.Client.Lobby
{
    public interface IClientPreferencesManager
    {
        event Action OnServerDataLoaded;

        bool ServerDataLoaded => Settings != null;

        GameSettings? Settings { get; }
        PlayerPreferences? Preferences { get; }
        void Initialize();
        void SelectCharacter(HumanoidCharacterProfile profile);
        void SelectCharacter(int slot);
        void UpdateCharacter(HumanoidCharacterProfile profile, int slot);
        void CreateCharacter(HumanoidCharacterProfile profile);
        void DeleteCharacter(HumanoidCharacterProfile profile);
        void DeleteCharacter(int slot);
        void SetCustomGhost(string ghostProto); // Orion
        void UpdateConstructionFavorites(List<ProtoId<ConstructionPrototype>> favorites);
    }
}
