---
name: xaml-ui
description: Build and maintain client XAML controls, windows, code-behind, styles, and resource references.
---

# Xaml Ui

XAML is client presentation. Keep game authority and protected validation out of code-behind.

## Workflow

1. Find a nearby control using the same UI framework.
2. Define layout and reusable styles in XAML.
3. Keep code-behind focused on binding state and raising UI intents.
4. Put shared messages and state in Shared for BUI/EUI flows.
5. Validate resource paths, control names, and generated XAML compilation.
6. Test scaling, long localization strings, disabled states, and window lifetime.

Do not place server services in client UI. Avoid hardcoded player-visible strings. Unsubscribe events when controls or windows close.
