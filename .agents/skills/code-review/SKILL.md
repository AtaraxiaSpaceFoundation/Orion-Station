---
name: code-review
description: Review changes for correctness, regressions, authority, architecture, compatibility, performance, and missing tests.
---

# Code Review

Review observable behavior before style.

## Order of review

1. Understand intended behavior and scope.
2. Identify owners and assembly boundaries.
3. Trace entry points, validation, mutation, dirtying, persistence, and presentation.
4. Check failure paths and lifecycle cleanup.
5. Review prototypes, localization, resources, and licenses.
6. Check compatibility and migration impact.
7. Evaluate tests and verification evidence.
8. Only then consider maintainability and style.

## High-value findings

Prioritize crashes, data loss, privilege bypass, desync, duplicated prediction, stale UI, invalid prototype references, missing dirtying, incorrect module placement, compatibility breaks, hot-path regressions, and untested behavior.

Every finding should include the path, failure mode, user or operator impact, and a minimal remediation. Distinguish confirmed defects from questions or speculative risks.

## Avoid

Do not flood the review with formatting preferences, restate the diff, or demand abstractions without a demonstrated problem. Do not approve based solely on compilation when runtime authority or resources changed.
