# Action lifecycle reference

Trace an action through:

`grant source -> action entity/state -> UI availability -> request -> validation -> optional DoAfter -> completion -> cooldown -> removal`

Review ownership of the action entity, duplicate grants, transfer between bodies, polymorph or mind changes, equipment removal, death, round restart, and reconnect.

An action source should be idempotent. Starting the same component twice must not grant duplicate actions.
