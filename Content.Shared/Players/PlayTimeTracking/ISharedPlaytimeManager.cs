using System.Diagnostics.CodeAnalysis; // Orion
using Robust.Shared.Player;

namespace Content.Shared.Players.PlayTimeTracking;

public interface ISharedPlaytimeManager
{
    /// <summary>
    /// Gets the playtimes for the session or an empty dictionary if none found.
    /// </summary>
    IReadOnlyDictionary<string, TimeSpan> GetPlayTimes(ICommonSession session);

    bool TryGetTrackerTimes(ICommonSession id, [NotNullWhen(true)] out Dictionary<string, TimeSpan>? time); // Orion
}
