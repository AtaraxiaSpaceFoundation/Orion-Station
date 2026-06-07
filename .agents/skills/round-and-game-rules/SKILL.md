---
name: round-and-game-rules
description: Implement round lifecycle, game rules, objectives, announcements, win conditions, and restart-safe cleanup.
---

# Round And Game Rules

Round systems coordinate global state and must remain deterministic, authority-safe, and restart-safe.

## Lifecycle

Trace rule activation, round start, player assignment, periodic updates, end conditions, round end, restart, and cleanup. Do not assume a system instance or cached entity survives only one round.

## Rule state

Keep authoritative rule state on the server. Replicate only information clients need. Prefer rule entities and existing game-rule APIs over global static state.

## Players and roles

Handle late join, reconnect, ghosting, body changes, mind transfer, disconnect, and administrative intervention. Assign objectives and roles through established systems and avoid duplicate assignment.

## Announcements and results

Localize announcements and summaries. Keep hidden antagonist or objective information restricted to intended recipients. Win conditions should be computed from current authoritative state and remain safe when participants are deleted or disconnected.

## Cleanup

Remove spawned entities, timers, subscriptions, overlays, temporary CVars, and round-scoped caches. Reset all state that could leak into the next round.

## Verification

Test activation, late join, no eligible players, disconnect, participant deletion, multiple simultaneous rules, early round end, normal round end, and restart.
