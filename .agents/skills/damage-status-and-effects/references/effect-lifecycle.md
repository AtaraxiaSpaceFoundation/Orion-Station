# Effect lifecycle

For each effect define:

- source and target;
- authoritative owner;
- initial application;
- refresh and stacking rule;
- duration and cancellation;
- derived modifiers;
- player presentation;
- removal and cleanup;
- behavior on death, deletion, body transfer, and round restart.

Use an application token or current-state check when an old timer could incorrectly remove a newer effect.
