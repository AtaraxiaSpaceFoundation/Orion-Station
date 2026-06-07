---
name: audio
description: Add data-driven audio, sound collections, predicted feedback, world playback, and licensed audio assets.
---

# Audio

Classify the sound before choosing an API:

- local predicted gameplay feedback;
- PVS world sound;
- moving entity source;
- static coordinate source;
- global filtered notification;
- client-only UI sound;
- ambient or music playback.

Prefer component fields, prototypes, `SoundSpecifier`, and sound collections over hardcoded paths. Keep client-only playback out of Shared dependencies.

For predicted actions, avoid playing the same sound once locally and once again on authoritative confirmation. Preserve asset attribution and verify collection IDs and paths.

Test range, repetition, cancellation, deletion of the source, and concurrent playback when relevant.
