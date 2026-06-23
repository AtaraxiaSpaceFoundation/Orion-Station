using Robust.Shared.Configuration;

namespace Content.Orion.Shared.CCVar;

public sealed partial class OrionCCVars
{
    public static readonly CVarDef<bool> EnableLightsGlowing =
        CVarDef.Create("light.light.enable_lights_glowing", true, CVar.CLIENTONLY | CVar.ARCHIVE);
}
