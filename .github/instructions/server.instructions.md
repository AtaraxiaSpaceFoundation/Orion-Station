---
applyTo: "Content.Server/**/*.cs,Modules/*/Content.*.Server*/**/*.cs"
---

Server code owns validation, protected decisions, persistence, and authoritative mutation. Validate every client-originated request.
