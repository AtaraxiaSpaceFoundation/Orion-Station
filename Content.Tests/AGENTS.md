# Content.Tests guidance

Write focused tests for isolated content behavior, validation rules, serialization, prototype assumptions, and regressions that do not require a full integrated server/client session.

Keep tests deterministic and name them after observable behavior. Reuse existing fixtures and helpers. Avoid broad setup when a smaller system-level test can prove the requirement.
