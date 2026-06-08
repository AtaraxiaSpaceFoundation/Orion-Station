// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.IntegrationTests;
using Content.IntegrationTests.Fixtures;
using Content.Orion.Server.CloningAppearance.Systems;
using Content.Server.Preferences.Managers;
using Content.Shared.Humanoid;
using Content.Shared.Interaction.Components;
using Content.Shared.Inventory;
using Content.Shared.Players;
using Content.Shared.Preferences;
using Robust.Shared.GameObjects;

namespace Content.Orion.IntegrationTests.Tests.CloningAppearance;

[TestOf(typeof(CloningAppearanceSystem))]
public sealed class CloningAppearanceSystemTest : GameTest
{
    private const string CloningAppearanceEntity = "CloningAppearanceTestEntity";
    private const string CloningAppearanceGear = "CloningAppearanceTestGear";
    private const string CloningAppearanceTrait = "CloningAppearanceTestTrait";
    private const string StartingGearJumpsuit = "CloningAppearanceTestJumpsuit";

    public override PoolSettings PoolSettings => new() { Connected = true, DummyTicker = false };

    [TestPrototypes]
    private const string Prototypes = $"""

        - type: entity
          id: {CloningAppearanceEntity}
          components:
          - type: CloningAppearance
            components:
            - type: Unremoveable
              deleteOnDrop: false
            startingGear: {CloningAppearanceGear}
            copyTraits: true

        - type: entity
          id: {StartingGearJumpsuit}
          parent: ClothingUniformJumpsuitColorGrey

        - type: startingGear
          id: {CloningAppearanceGear}
          equipment:
            jumpsuit: {StartingGearJumpsuit}

        - type: trait
          id: {CloningAppearanceTrait}
          name: generic-unknown
          components:
          - type: BlockMovement
            blockInteraction: false

        """;

    [Test]
    public async Task AttachingPlayerSpawnsProfileCopiesComponentsTraitsAndStartingGear()
    {
        var testMap = await Pair.CreateTestMap();
        var session = ServerSession;

        Assert.That(session, Is.Not.Null);
        Assert.That(session!.AttachedEntity, Is.Not.Null);

        if (session.AttachedEntity is not { } originalBody)
        {
            Assert.Fail("The player did not start with an attached body.");
            return;
        }
        var originalMind = session.GetMind();
        var preferences = Server.ResolveDependency<IServerPreferencesManager>();
        var inventory = SEntMan.System<InventorySystem>();
        var cloningEntity = await SpawnAtPosition(CloningAppearanceEntity, testMap.GridCoords);
        HumanoidCharacterProfile originalProfile = default;
        var selectedCharacterIndex = 0;

        await Server.WaitPost(() =>
        {
            var playerPreferences = preferences.GetPreferences(session.UserId);
            originalProfile = playerPreferences.SelectedCharacter;
            selectedCharacterIndex = playerPreferences.SelectedCharacterIndex;
        });

        try
        {
            await Server.WaitPost(() =>
            {
                var profileWithTrait = originalProfile.WithTraitPreference(CloningAppearanceTrait, SProtoMan);

                Assert.That(profileWithTrait.TraitPreferences.Contains(CloningAppearanceTrait), Is.True);
                preferences.SetProfile(session.UserId, selectedCharacterIndex, profileWithTrait).Wait();
                Server.PlayerMan.SetAttachedEntity(session, cloningEntity);
            });
            await RunTicksSync(1);

            await Server.WaitAssertion(() =>
            {
                if (session.AttachedEntity is not { } clonedBody)
                {
                    Assert.Fail("The player was not attached to a cloned body.");
                    return;
                }

                var hasCopiedComponent = SEntMan.TryGetComponent(clonedBody, out UnremoveableComponent copiedComponent);
                var hasTraitComponent = SEntMan.TryGetComponent(clonedBody, out BlockMovementComponent traitComponent);
                var hasStartingGear = inventory.TryGetSlotEntity(clonedBody, "jumpsuit", out var jumpsuit);
                var jumpsuitPrototype = hasStartingGear && jumpsuit is { } jumpsuitUid
                    ? SEntMan.GetComponent<MetaDataComponent>(jumpsuitUid).EntityPrototype?.ID
                    : null;

                using (Assert.EnterMultipleScope())
                {
                    Assert.That(SEntMan.Deleted(cloningEntity), Is.True);
                    Assert.That(clonedBody, Is.Not.EqualTo(originalBody).And.Not.EqualTo(cloningEntity));
                    Assert.That(SEntMan.HasComponent<HumanoidProfileComponent>(clonedBody), Is.True);
                    Assert.That(hasCopiedComponent, Is.True);
                    Assert.That(copiedComponent?.DeleteOnDrop, Is.False);
                    Assert.That(hasTraitComponent, Is.True);
                    Assert.That(traitComponent?.BlockInteraction, Is.False);
                    Assert.That(hasStartingGear, Is.True);
                    Assert.That(jumpsuitPrototype, Is.EqualTo(StartingGearJumpsuit));
                    Assert.That(session.GetMind(), Is.EqualTo(originalMind));
                }
            });
        }
        finally
        {
            await Server.WaitPost(() => preferences.SetProfile(session.UserId, selectedCharacterIndex, originalProfile).Wait());
        }
    }
}
