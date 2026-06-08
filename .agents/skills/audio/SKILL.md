---
name: audio
description: Add data-driven audio with correct prediction, audience, resources, attribution, and verification.
---

<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <103608145+PuroSlavKing@users.noreply.github.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Audio

Classify the sound before selecting an API: predicted local feedback, PVS world sound, moving entity source, static coordinate source, global notification, client-only UI sound, ambient loop, or music.

Prefer component fields, prototypes, `SoundSpecifier`, and sound collections over hardcoded paths. Keep client-only playback out of Shared dependencies.

Predicted actions must not play the same sound locally and again on authoritative confirmation. Verify source deletion, cancellation, range, repetition, and concurrent playback.

Add both `en-US` and `ru-RU` when the audio feature introduces player-visible captions, UI labels, announcements, examine text, or settings.

Preserve source attribution and asset-specific license metadata without editing SPDX.

## Verification commands

```powershell
dotnet restore
dotnet build --configuration Debug --no-restore /m
dotnet build --configuration Release --no-restore /p:WarningsAsErrors= /m
dotnet run --project Content.YAMLLinter/Content.YAMLLinter.csproj --no-build
git diff --check
```

Run the owning integration tests for predicted or networked playback behavior.
