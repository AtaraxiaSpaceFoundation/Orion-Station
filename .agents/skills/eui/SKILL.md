---
name: eui
description: Implement EUI sessions, shared state and messages, server lifecycle, and client presentation.
---

# Eui

Use EUI for session-oriented interfaces that are not naturally bound to one world entity or an existing BUI owner.

## Shared contract

Define serializable state and intent messages in Shared. Keep them minimal and free of hidden server-only data.

## Server lifecycle

The server creates the session, checks authorization, sends state, handles messages, revalidates permissions, and closes the session. Handle disconnects, permission changes, target deletion, and explicit shutdown.

## Client lifecycle

The client creates presentation, applies state, sends intent, and disposes windows and subscriptions on close. Do not treat client button state as authorization.

## Choosing EUI or BUI

Use BUI when the interaction is clearly owned by an entity and normal range/lifecycle semantics fit. Use EUI for broader admin, debug, lobby, or session-oriented interfaces.

## Verification

Test open, update, rejected message, repeated message, disconnect, server close, client close, and stale session behavior. Check localization and long text layout.
