// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
// SPDX-FileCopyrightText: 2026 RedFoxIV <38788538+redfoxiv@users.noreply.github.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Shared.Construction.Prototypes;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared.Preferences
{
    /// <summary>
    ///     Contains all player characters and the index of the currently selected character.
    ///     Serialized both over the network and to disk.
    /// </summary>
    [Serializable]
    [NetSerializable]
    public sealed class PlayerPreferences
    {
        private Dictionary<int, HumanoidCharacterProfile> _characters;

        public PlayerPreferences(IEnumerable<KeyValuePair<int, HumanoidCharacterProfile>> characters, int selectedCharacterIndex, Color adminOOCColor, List<ProtoId<ConstructionPrototype>> constructionFavorites, string customGhost = "default") // Orion-Edit: customGhost
        {
            _characters = new Dictionary<int, HumanoidCharacterProfile>(characters);
            SelectedCharacterIndex = selectedCharacterIndex;
            AdminOOCColor = adminOOCColor;
            ConstructionFavorites = constructionFavorites;
            CustomGhost = customGhost; // Orion
        }

        /// <summary>
        ///     All player characters.
        /// </summary>
        public IReadOnlyDictionary<int, HumanoidCharacterProfile> Characters => _characters;

        public HumanoidCharacterProfile GetProfile(int index)
        {
            return _characters[index];
        }

        /// <summary>
        ///     Index of the currently selected character.
        /// </summary>
        public int SelectedCharacterIndex { get; }

        /// <summary>
        ///     The currently selected character.
        /// </summary>
        public HumanoidCharacterProfile SelectedCharacter => Characters[SelectedCharacterIndex];

        public Color AdminOOCColor { get; set; }
        public string CustomGhost { get; set; } // Orion

        // Orion-Start
        public PlayerPreferences WithCustomGhost(string customGhost) =>
            new(_characters, SelectedCharacterIndex, AdminOOCColor, ConstructionFavorites, customGhost);
        // Orion-End

        /// <summary>
        ///    List of favorite items in the construction menu.
        /// </summary>
        public List<ProtoId<ConstructionPrototype>> ConstructionFavorites { get; set; } = [];

        public int IndexOfCharacter(HumanoidCharacterProfile profile)
        {
            return _characters.FirstOrNull(p => p.Value == profile)?.Key ?? -1;
        }

        public bool TryIndexOfCharacter(HumanoidCharacterProfile profile, out int index)
        {
            return (index = IndexOfCharacter(profile)) != -1;
        }
    }
}
