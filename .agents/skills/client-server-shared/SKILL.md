---
name: client-server-shared
description: Place contracts and behavior correctly across trust, prediction, presentation, and authority boundaries.
---

# Client Server Shared

Start from the trust boundary.

## Shared

Shared owns components visible to both sides, common events, network payloads, BUI contracts, and logic safe to execute during prediction. Shared code may run repeatedly on the client.

## Server

Server validates requests, owns hidden and persistent state, selects protected outcomes, controls economy and access, and performs authoritative mutation.

## Client

Client presents state, renders visuals, handles XAML and controls, and provides immediate local feedback. Client checks improve UX but are never security checks.

## Review questions

- Does the client receive more information than it needs?
- Is a protected result selected client-side?
- Can the same shared effect execute twice?
- Does a server mutation need dirtying?
- Is client-only UI leaking into Shared?
- Is server-only persistence leaking into a shared component?
