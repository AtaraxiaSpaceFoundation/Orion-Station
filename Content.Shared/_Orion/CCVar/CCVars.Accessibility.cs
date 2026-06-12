using Robust.Shared.Configuration;

// ReSharper disable once CheckNamespace
namespace Content.Shared.CCVar;

public sealed partial class CCVars
{
    /// <summary>
    /// Controls whether combat mode indicators are displayed above entities.
    /// </summary>
    public static readonly CVarDef<bool> CombatIndicator =
        CVarDef.Create("accessibility.CombatIndicator", true, CVar.CLIENTONLY | CVar.ARCHIVE);
}
