# XAML structure

Keep named controls limited to elements code-behind actually accesses. Prefer containers and existing styles over manual pixel positioning.

Code-behind should expose methods such as `UpdateState`, translate user actions into events, and avoid owning authoritative state.

When a window is recreated or rebound, prevent duplicate event subscriptions. Verify XAML resource URIs and the owning client project's XamlIL setup.
