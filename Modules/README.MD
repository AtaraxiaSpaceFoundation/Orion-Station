<!--
SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>

SPDX-License-Identifier: AGPL-3.0-or-later
-->

# Module System

The module system allows you to organize custom content into self-contained modules with a manifest-based configuration system.

## Directory Structure

All modules should be placed in the `Modules/` directory:

```
Modules/
├── YourModule/
│   ├── module.yml                          # Module manifest (required)
│   ├── Content.YourModule.Shared/
│   │   ├── Content.YourModule.Shared.csproj
│   │   └── ... your shared code ...
│   ├── Content.YourModule.Client/
│   │   ├── Content.YourModule.Client.csproj
│   │   └── ... your client code ...
│   └── Content.YourModule.Server/
│       ├── Content.YourModule.Server.csproj
│       └── ... your server code ...
└── AnotherModule/
    └── ...
```

## Module Manifest (`module.yml`)

Every module must have a `module.yml` file at its root. This file defines the module's metadata and which projects belong to it.

### Schema

```yaml
name: YourModule                     # Human-readable module name
id: your_module                      # Unique identifier (lowercase, alphanumeric + underscore)
version: 1.0.0                       # Version (most people will never use this, i figured I'd add it because why not?)

# Project mappings (paths relative to module.yml location)
projects:
  - path: Content.YourModule.Shared  # Relative path to .csproj directory
    role: Shared                     # Role: Client, Server, Shared, or Common

  - path: Content.YourModule.Client
    role: Client

  - path: Content.YourModule.Server
    role: Server

  - path: Content.YourModule.Common
    role: Common
```


## Creating a New Module

### Step 1: Create Module Directory

```bash
mkdir -p Modules/YourModule
```

### Step 2: Create `module.yml`

Create `Modules/YourModule/module.yml`:

```yaml
name: YourModule
id: your_module
version: 1.0.0

projects:
  - path: Content.YourModule.Shared
    role: Shared
```

### Step 3: Create Project Directories

```bash
mkdir -p Modules/YourModule/Content.YourModule.Shared
```

### Step 4: Create `.csproj` Files

**Shared Project** (`Content.YourModule.Shared/Content.YourModule.Shared.csproj`):

```xml
<Project Sdk="Microsoft.NET.Sdk">
    <PropertyGroup>
        <Nullable>enable</Nullable>
        <ImplicitUsings>enable</ImplicitUsings>
        <RootNamespace>Content.YourModule.Shared</RootNamespace>
    </PropertyGroup>

    <ItemGroup>
        <ProjectReference Include="../../../Content.Shared/Content.Shared.csproj" />
    </ItemGroup>
</Project>
```

Writing TODO:

- [ ] Step 5: Add to SLN (`find . -name "*.csproj" ! -path "./RobustToolbox/*" -exec dotnet sln add {} \;`)
- [ ] Step 6: IoC + Entrypoint
- [ ] Step 7: Reference in parent module if necessary
- [ ] Step 8: uhm uhm uhm
- [ ] Also I think i'll prolly write a script to generate a new module with Client/Shared/Server/Common modules baked in
- [ ] uhm.... explain dependencies, circular shit, and common

## Modular Integration Tests

Integrated behavior owned by a module should be tested in `Modules/YourModule/Content.YourModule.IntegrationTests`, rather than in root `Content.IntegrationTests`. A typical project contains:

```text
Content.YourModule.IntegrationTests/
├── Content.YourModule.IntegrationTests.csproj
├── AssemblyInfo.cs
├── GlobalUsings.cs
├── YourModulePoolManagerTestEventHandler.cs
└── Tests/
```

The project is a test-only compile-time owner. It references `Content.Benchmarks` for the shared pool setup and only the Client, Server, Shared, or Common projects from its module that its test code directly uses. All runtime modules are loaded into the integration-test environment, but their types cannot be referenced by test code without a `ProjectReference`; do not add cross-module references merely for convenience.

Match the current modular test projects and import `MSBuild/Content.props`, RobustToolbox Client, Server, Shared, and Testing props, `Robust.Properties.targets`, `Robust.CompNetworkGenerator.targets`, `MSBuild/AllModules.targets`, and `MSBuild/TestModules.targets`. The setup fixture should call the shared `IntegrationTestHelpers.ChangeRootDir`, `PoolManagerHelpers.Setup`, and `PoolManager.Shutdown` APIs rather than implementing another pool.

The integration-test project is not a runtime module project and must not be added to `module.yml`. Add it to the module folder in `SpaceStation14.slnx` and add a dedicated invocation beside the other modular tests in `.github/workflows/build-test-debug.yml`.

Run Orion integration tests locally with:

```sh
dotnet test --no-build --configuration Debug Modules/Orion/Content.Orion.IntegrationTests/Content.Orion.IntegrationTests.csproj -- NUnit.ConsoleOut=0 NUnit.MapWarningTo=Failed
```
