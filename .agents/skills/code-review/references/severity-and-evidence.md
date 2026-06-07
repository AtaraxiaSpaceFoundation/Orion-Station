# Severity and evidence

Use severity from impact, reachability, and recoverability.

- Critical: privilege compromise, widespread data loss, or repository-breaking behavior.
- High: exploitable authority bug, common crash, severe desync, or irreversible state corruption.
- Medium: reproducible incorrect behavior, compatibility break, or meaningful operational failure.
- Low: localized maintenance issue with a credible future failure mode.

Support findings with code paths, tests, logs, or a concrete execution sequence. Do not label hypothetical style concerns as defects.
