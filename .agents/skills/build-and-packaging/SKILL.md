---
name: build-and-packaging
description: Work with solution structure, CI configurations, generated content, packaging, and release outputs.
---

# Build And Packaging

Use this skill when changing project files, solution membership, build targets, CI jobs, packaging rules, or generated assets.

## Repository baseline

- SDK version comes from `global.json`.
- The solution is `SpaceStation14.slnx`.
- Normal CI builds and tests in `Debug`.
- YAML linting uses a `Release` build followed by `--no-build` execution.
- Packaging is owned by `Content.Packaging` and related workflows.

## Project changes

Before adding a project reference, verify dependency direction and module ownership. Keep project names, root namespaces, content references, XAML configuration, and solution grouping consistent with nearby projects.

Do not add a base-to-module reference. Do not make Shared depend on Server or Client. Do not include module resources through ad hoc copy rules when the module manifest already defines them.

## CI changes

Keep workflows least-privileged. Pin actions according to repository policy. Preserve cache keys, submodule initialization, and platform-specific requirements. Do not hide failing tests with `continue-on-error` or broad warning suppression.

## Packaging

Confirm which client, server, symbols, resources, and module files belong in the output. Test both missing-file and duplicate-file risks. Avoid absolute paths and machine-specific assumptions.

## Verification

Run restore, affected build configurations, packaging checks, and inspect output manifests. Report platform or toolchain checks that were unavailable.
