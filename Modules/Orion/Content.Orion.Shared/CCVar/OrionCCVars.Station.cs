using Robust.Shared.Configuration;

namespace Content.Orion.Shared.CCVar;

[CVarDefs]
public sealed class OrionCCVars
{
    /*
     * Station Goal
     */

    /// <summary>
    /// Send station goal on round start or not.
    /// </summary>
    public static readonly CVarDef<bool> StationGoal =
        CVarDef.Create("game.station_goal", true, CVar.SERVERONLY);
}
