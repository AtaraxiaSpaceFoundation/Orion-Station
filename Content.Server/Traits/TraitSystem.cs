using Content.Shared.GameTicking;
using Content.Shared.Hands.Components;
using Content.Shared.Hands.EntitySystems;
using Content.Shared.Humanoid;
using Content.Shared.Preferences;
using Content.Shared.Roles;
using Content.Shared.Whitelist;
using Robust.Shared.Prototypes;

namespace Content.Server.Traits;

public sealed partial class TraitSystem : EntitySystem
{
    [Dependency] private IPrototypeManager _prototypeManager = default!;
    [Dependency] private SharedHandsSystem _sharedHandsSystem = default!;
    [Dependency] private EntityWhitelistSystem _whitelistSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PlayerSpawnCompleteEvent>(OnPlayerSpawnComplete);
    }

    // When the player is spawned in, add all trait components selected during character creation
    private void OnPlayerSpawnComplete(PlayerSpawnCompleteEvent args)
    {
        // Check if player's job allows to apply traits
        if (args.JobId == null ||
            !_prototypeManager.Resolve<JobPrototype>(args.JobId, out var protoJob) ||
            !protoJob.ApplyTraits)
        {
            return;
        }

        ApplyTraits(args.Mob, args.Profile); // Orion
    } // Orion-Edit: Separated ApplyTraits

    // Orion-Edit-Start: Separated from OnPlayerSpawnComplete
    /// <summary>
    /// Applies every valid trait in the profile, skipping missing trait prototypes instead of aborting the remaining traits.
    /// </summary>
    public void ApplyTraits(EntityUid mob, HumanoidCharacterProfile? profile)
    {
        // Orion-Start
        if (!Exists(mob))
        {
            Log.Error($"Cannot apply traits to missing entity {mob}.");
            return;
        }

        if (!HasComp<HumanoidProfileComponent>(mob))
        {
            Log.Error($"Cannot apply traits to non-humanoid entity {ToPrettyString(mob)}.");
            return;
        }

        if (profile == null)
        {
            Log.Error($"Cannot apply traits to {ToPrettyString(mob)} without a character profile.");
            return;
        }

        if (profile.TraitPreferences.Count == 0)
            return;

        var skippedTraitCount = 0;
        // Orion-End

        foreach (var traitId in profile.TraitPreferences)
        {
            if (!_prototypeManager.TryIndex(traitId, out var traitPrototype))
            {
                skippedTraitCount++;
                Log.Error($"No trait found with ID {traitId} for {ToPrettyString(mob)}; skipping it.");
                continue;
            }

            if (_whitelistSystem.IsWhitelistFail(traitPrototype.Whitelist, mob) ||
                _whitelistSystem.IsWhitelistPass(traitPrototype.Blacklist, mob))
                continue;

            // Add all components required by the prototype
            if (traitPrototype.Components.Count > 0)
                EntityManager.AddComponents(mob, traitPrototype.Components, false);

            // Add all JobSpecials required by the prototype
            foreach (var special in traitPrototype.Specials)
            {
                special.AfterEquip(mob);
            }

            // Add item required by the trait
            if (traitPrototype.TraitGear == null)
                continue;

            if (!TryComp(mob, out HandsComponent? handsComponent))
                continue;

            var coords = Transform(mob).Coordinates;
            var inhandEntity = Spawn(traitPrototype.TraitGear, coords);
            _sharedHandsSystem.TryPickup(mob,
                inhandEntity,
                checkActionBlocker: false,
                handsComp: handsComponent);
        }

        // Orion-Start
        if (skippedTraitCount > 0)
            Log.Info($"Skipped {skippedTraitCount} missing trait prototype(s) while applying traits to {ToPrettyString(mob)}.");
        // Orion-End
    }
    // Orion-Edit-End
}
