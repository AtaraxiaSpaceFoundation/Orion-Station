# Skill routing and preflight

Before editing:

1. Read root `AGENTS.md` and every scoped `AGENTS.md` that applies to the intended files.
2. Use `.agents/CATALOG.md` to select only the task-specific skills needed.
3. Inspect the current implementation and nearby patterns before proposing a new one.
4. Confirm paths, APIs, project references, and resource roots against the current branch.
5. Identify required verification before implementation so the design remains testable.

Do not load every skill by default. Excess context can hide the instructions that actually matter. Load one routing skill such as `gameplay-feature`, then the narrow technical skills for the affected layers.

Refresh assumptions when the branch changes, a dependency is updated, or a source port targets a different revision. Never rely on a path or API remembered from `Orion-Station-14` without confirming it exists here.
