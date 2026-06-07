# Orion Common guidance

Keep this project limited to types that do not require gameplay Shared, Server, or Client assemblies.

Suitable contents include low-level identifiers, pure data shapes, and contracts required below Shared. Do not place entity systems, networked components, BUI logic, client visuals, or server authority here.

Before adding a type, confirm Orion Shared cannot own it without creating an invalid dependency.
