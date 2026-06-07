---
name: ai-workflow
description: Plan, delegate, verify, and report complex repository work without losing evidence or scope.
---

# AI Workflow

Use this skill for broad audits, multi-layer features, migrations, or tasks delegated to subagents.

## Establish the contract

Translate the request into observable outcomes, exclusions, target branch, expected commit structure, and required checks. Resolve repository identity first: `Orion-Station` is Goob Reforged-based and is not the same project as `Orion-Station-14`.

## Explore before editing

Ask an explorer to map ownership, entry points, dependencies, resources, tests, and current behavior. Require paths and symbols rather than a general summary. Confirm unstable framework APIs against current code or official documentation.

## Decompose by responsibility

Split work into independent questions such as architecture, networking, UI, resources, tests, and licensing. Avoid having multiple agents edit overlapping files. Keep one implementation owner responsible for integrating conclusions.

## Verify claims

Every important claim should come from a file, command result, test, or clearly labeled inference. Run focused checks first, then broader checks according to risk. A missing tool is a limitation, not a pass.

## Handoff

Summarize behavior, architecture decisions, changed files, verification, remaining risks, and delivery status. Confirm whether a commit was actually pushed instead of treating an internal task record as a GitHub update.
