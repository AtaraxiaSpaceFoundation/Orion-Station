# Base Server guidance

Root Server is authoritative base content. Changes here have broad impact.

Validate client actions, preserve persistence and security boundaries, and avoid module-specific branches. If only Orion needs the behavior, implement it in `Modules/Orion/Content.Orion.Server` or add a narrow reusable extension point here.
