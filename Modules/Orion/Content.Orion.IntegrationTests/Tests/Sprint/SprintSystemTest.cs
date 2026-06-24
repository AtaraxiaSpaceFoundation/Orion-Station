// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.IntegrationTests;
using Content.IntegrationTests.Fixtures;
using Content.Orion.Shared.Sprint;
using Content.Orion.Shared.Sprint.Systems;
using Content.Shared.Damage.Components;
using Content.Shared.Damage.Systems;
using Content.Shared.Movement.Components;
using Content.Shared.Movement.Systems;
using Robust.Shared.GameObjects;
using Robust.Shared.Timing;

namespace Content.Orion.IntegrationTests.Tests.Sprint;

[TestFixture]
public sealed partial class SprintSystemTest : GameTest
{
    private const string SprintTestEntity = "SprintTestEntity";
    private const string SprintTestEntityNoStamina = "SprintTestEntityNoStamina";

    [TestPrototypes]
    private const string Prototypes = $"""
        - type: entity
          id: {SprintTestEntity}
          parent: BaseMob
          components:
          - type: Sprint
          - type: Stamina
            decay: 100
            cooldown: 0
          - type: InputMover

        - type: entity
          id: {SprintTestEntityNoStamina}
          parent: BaseMob
          components:
          - type: Sprint
          - type: InputMover
        """;

    [Test]
    public async Task StartSprintRequiresMinStamina()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntity);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);
            var stamina = SEntMan.GetComponent<StaminaComponent>(ent);

            // With 0 stamina damage (full stamina), sprint should start
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.True);
            sSystem.StopSprint(ent, sprint);
            Assert.That(sprint.IsSprinting, Is.False);

            // Set stamina below minimum threshold
            stamina.StaminaDamage = stamina.CritThreshold - sprint.MinStaminaToSprint + 1;
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.False,
                "Should not sprint when stamina is below minimum");
        });
    }

    [Test]
    public async Task SprintStopsWhenStaminaDepleted()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntity);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);
            var stamina = SEntMan.GetComponent<StaminaComponent>(ent);

            // Set stamina close to depletion
            stamina.StaminaDamage = stamina.CritThreshold - 1;

            // Start sprint - should succeed since stamina >= MinStaminaToSprint (50)
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.True);
            Assert.That(sprint.IsSprinting, Is.True);

            // Drain the remaining stamina
            var staminaSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedStaminaSystem>();
            staminaSystem.TakeStaminaDamage(ent, stamina.StaminaDamage + 100, stamina);

            // The sprint system's Update should detect this and stop sprinting
            sSystem.Update(1.0f);
            Assert.That(sprint.IsSprinting, Is.False);
        });
    }

    [Test]
    public async Task CooldownPreventsImmediateRestart()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntity);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);
            var stamina = SEntMan.GetComponent<StaminaComponent>(ent);

            // Start sprint
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.True);

            // Stop sprint - should start cooldown
            sSystem.StopSprint(ent, sprint);
            Assert.That(sprint.IsOnCooldown, Is.True);
            Assert.That(sprint.CooldownRemaining, Is.GreaterThan(0));

            // Can't start sprint during cooldown
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.False);
        });
    }

    [Test]
    public async Task CooldownExpiresAfterDuration()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntity);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);
            var stamina = SEntMan.GetComponent<StaminaComponent>(ent);

            // Start and stop sprint to initiate cooldown
            sSystem.TryStartSprint(ent, sprint, stamina);
            sSystem.StopSprint(ent, sprint);
            Assert.That(sprint.IsOnCooldown, Is.True);
            Assert.That(sprint.CooldownRemaining, Is.EqualTo(sprint.CooldownTime));

            // Advance time past cooldown
            var remaining = sprint.CooldownRemaining;
            sSystem.Update(remaining + 0.1f);
            Assert.That(sprint.IsOnCooldown, Is.False);
            Assert.That(sprint.CooldownRemaining, Is.EqualTo(0));

            // Can sprint again
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.True);
        });
    }

    [Test]
    public async Task CannotSprintWhenCanMoveIsFalse()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntity);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);
            var stamina = SEntMan.GetComponent<StaminaComponent>(ent);

            // Set CanMove to false
            var mover = SEntMan.GetComponent<InputMoverComponent>(ent);
            mover.CanMove = false;

            // Should not be able to sprint
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.False);

            // Restore CanMove
            mover.CanMove = true;
            Assert.That(sSystem.TryStartSprint(ent, sprint, stamina), Is.True);
        });
    }

    [Test]
    public async Task SprintAppliesSpeedModifier()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntity);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();
        var moveSpeedSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<MovementSpeedModifierSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);
            var stamina = SEntMan.GetComponent<StaminaComponent>(ent);
            var moveMod = SEntMan.GetComponent<MovementSpeedModifierComponent>(ent);

            // Record base sprint speed modifier
            var baseSprintMod = moveMod.SprintSpeedModifier;

            // Start sprint
            sSystem.TryStartSprint(ent, sprint, stamina);
            moveSpeedSystem.RefreshMovementSpeedModifiers(ent);

            // Sprint speed should be multiplied
            Assert.That(moveMod.SprintSpeedModifier, Is.EqualTo(baseSprintMod * sprint.SprintMultiplier));

            // Stop sprint
            sSystem.StopSprint(ent, sprint);
            moveSpeedSystem.RefreshMovementSpeedModifiers(ent);

            // Speed should return to normal
            Assert.That(moveMod.SprintSpeedModifier, Is.EqualTo(baseSprintMod));
        });
    }

    [Test]
    public async Task RequiresSprintComponent()
    {
        var testMap = await Pair.CreateTestMap();
        var ent = await Spawn(SprintTestEntityNoStamina);
        var sSystem = Server.ResolveDependency<IEntitySystemManager>().GetEntitySystem<SharedSprintSystem>();

        await Server.WaitAssertion(() =>
        {
            var sprint = SEntMan.GetComponent<SprintComponent>(ent);

            // Entity has SprintComponent but no StaminaComponent
            Assert.That(SEntMan.HasComponent<StaminaComponent>(ent), Is.False);

            // Should not be able to sprint without stamina
            Assert.That(sSystem.TryStartSprint(ent, sprint), Is.False);
        });
    }
}
