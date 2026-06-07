# Round lifecycle reference

Review the complete path:

`rule selection -> activation -> player assignment -> active updates -> end condition -> announcement/results -> cleanup -> restart`

At each phase, identify state owner, timers, spawned entities, recipient filters, persistence writes, and failure behavior when no participant remains.
