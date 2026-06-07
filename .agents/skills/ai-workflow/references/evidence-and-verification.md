# Evidence and verification

Rank evidence from strongest to weakest:

1. successful automated test or reproducible runtime behavior;
2. current source code and project configuration;
3. official version-specific documentation;
4. repository history and source PR discussion;
5. analogy to nearby code;
6. memory or assumption.

State when a conclusion relies on a weaker source. Re-check paths and APIs after branch updates. For a failed command, preserve the exact failure and avoid claiming downstream checks were performed.
