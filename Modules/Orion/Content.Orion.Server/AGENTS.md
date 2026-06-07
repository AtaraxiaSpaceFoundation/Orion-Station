# Orion Server guidance

Server owns authoritative Orion outcomes, validation, persistence coordination, hidden information, anti-abuse checks, and server-only simulation.

Validate all client messages. Re-resolve network entities and re-check state at execution time. Do not trust UI validation, displayed prices, access cached on the client, or client-selected results.

Keep presentation and XAML out of this project. Send limited BUI state or events rather than exposing complete server components.
