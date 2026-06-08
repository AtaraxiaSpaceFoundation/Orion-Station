using Content.Orion.Server.CloningAppearance.Components;
using Content.Orion.Server.CloningAppearance.Events;
using Content.Server.Clothing.Systems;
using Content.Server.GameTicking;
using Content.Server.Mind;
using Content.Server.Station.Systems;
using Content.Server.Traits;
using Content.Shared.Bed.Cryostorage;
using Content.Shared.Mind;
using Content.Shared.Mind.Components;
using Content.Shared.Preferences;
using Content.Shared.Station.Components;
using Robust.Shared.Containers;
using Robust.Shared.GameObjects;
using Robust.Shared.IoC;
using Robust.Shared.Map;
using Robust.Shared.Player;
using Robust.Shared.Serialization.Manager;

namespace Content.Orion.Server.CloningAppearance.Systems;

public sealed partial class CloningAppearanceSystem : EntitySystem
{
    [Dependency] private ISerializationManager _serialization = default!;
    [Dependency] private StationSystem _stations = default!;
    [Dependency] private StationSpawningSystem _spawning = default!;
    [Dependency] private GameTicker _ticker = default!;
    [Dependency] private MindSystem _mindSystem = default!;
    [Dependency] private EntityLookupSystem _entityLookupSystem = default!;
    [Dependency] private SharedContainerSystem _container = default!;
    [Dependency] private OutfitSystem _outfitSystem = default!;
    [Dependency] private TraitSystem _traitSystem = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CloningAppearanceComponent, PlayerAttachedEvent>(OnPlayerAttached);
        SubscribeLocalEvent<CloningAppearanceEvent>(OnPlayerSpawn);
    }

    private EntityUid SpawnProfileEntity(EntityCoordinates coordinates, HumanoidCharacterProfile? profile, EntityUid? stationUid = null)
    {
        if (profile == null)
        {
            Log.Error("Cannot spawn a cloning appearance entity without a character profile.");
            return EntityUid.Invalid;
        }

        if (!coordinates.IsValid(EntityManager))
        {
            Log.Error($"Cannot spawn a cloning appearance entity at invalid coordinates {coordinates}.");
            return EntityUid.Invalid;
        }

        if (stationUid == null || (Exists(stationUid.Value) && HasComp<StationDataComponent>(stationUid.Value)))
            return _spawning.SpawnPlayerMob(coordinates, null, profile, stationUid);

        Log.Error($"Cannot spawn a cloning appearance entity for invalid station {stationUid.Value}.");
        return EntityUid.Invalid;

    }

    private void OnPlayerSpawn(CloningAppearanceEvent ev)
    {
        var profile = _ticker.GetPlayerProfile(ev.Player);
        var mobUid = SpawnProfileEntity(ev.Coords, profile, ev.StationUid);
        if (!Exists(mobUid))
        {
            Log.Error($"Failed to spawn a cloning appearance entity for player {ev.Player.Name}.");
            return;
        }

        var targetMind = ev.MindId != null && TryComp<MindComponent>(ev.MindId, out var transferredMind)
            ? (ev.MindId.Value, transferredMind)
            : _mindSystem.GetOrCreateMind(ev.Player.UserId);

        foreach (var entry in ev.Component.Components.Values)
        {
            var comp = (Component) _serialization.CreateCopy(entry.Component, notNullableOverride: true);
            AddComp(mobUid, comp, true);
        }

        if (ev.Component.StartingGear != null)
            _outfitSystem.SetOutfit(mobUid, ev.Component.StartingGear);

        if (ev.Component.CopyTraits)
            _traitSystem.ApplyTraits(mobUid, profile);

        var owningStation = _stations.GetOwningStation(mobUid);
        foreach (var nearbyEntity in _entityLookupSystem.GetEntitiesInRange(mobUid, 1f))
        {
            // Try insert into cryo storage
            if (owningStation == null || _stations.GetOwningStation(nearbyEntity) != owningStation || !TryComp<CryostorageComponent>(nearbyEntity, out var cryostorageComponent))
            {
                continue;
            }

            if (!_container.TryGetContainer(nearbyEntity, cryostorageComponent.ContainerId, out var container))
                continue;

            if (!_container.CanInsert(mobUid, container, true))
                continue;

            _container.Insert(mobUid, container);
            break;
        }

        targetMind.Comp.CharacterName = MetaData(mobUid).EntityName;
        targetMind.Comp.OriginalOwnedEntity = GetNetEntity(mobUid);
        Dirty(targetMind);
        _mindSystem.TransferTo(targetMind, mobUid);
    }

    private void OnPlayerAttached(Entity<CloningAppearanceComponent> ent, ref PlayerAttachedEvent args)
    {
        if (TerminatingOrDeleted(ent))
            return;

        var componentSnapshot = _serialization.CreateCopy(ent.Comp, notNullableOverride: true);
        QueueLocalEvent(new CloningAppearanceEvent
        {
            Player = args.Player,
            Component = componentSnapshot,
            StationUid = _stations.GetOwningStation(ent),
            Coords = Transform(ent).Coordinates,
            MindId = TryComp<MindContainerComponent>(ent, out var mindContainer) ? mindContainer.Mind : null,
        });

        QueueDel(ent);
    }
}
