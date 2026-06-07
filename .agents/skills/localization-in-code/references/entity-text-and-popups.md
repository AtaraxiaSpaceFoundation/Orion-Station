# Entity text and popups

Before composing a message, decide:

- who receives it;
- whether the action is predicted;
- whether the entity name needs grammar processing;
- whether hidden server information is involved;
- whether the same reason is reused in UI state.

Use predicted popup helpers for predicted local interactions and authoritative popup helpers for server-only outcomes. Avoid sending both for the same successful action unless de-duplication is deliberate.
