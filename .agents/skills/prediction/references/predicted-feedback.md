# Predicted feedback

For local actions, prefer the repository's predicted popup and audio helpers instead of a client-only call plus an authoritative call.

Review every side effect:

- component mutation;
- entity spawn or deletion;
- audio;
- popup;
- animation;
- cooldown;
- UI update.

Decide whether it is deterministic, first-time-only, server-only, or reconciled through replicated state. Duplicate sound or popup is usually evidence that a side effect ran in both prediction and confirmation without de-duplication.
