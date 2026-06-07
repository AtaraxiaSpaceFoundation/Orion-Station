# Integration test guidance

Use integration tests for behavior that crosses server, client, networking, maps, prototypes, or multiple systems.

Control time and randomness. Assert authoritative results and, when relevant, replicated client state. Keep setup minimal and clean up spawned entities or sessions. Add an integration test when a regression cannot be represented faithfully as a focused unit test.
