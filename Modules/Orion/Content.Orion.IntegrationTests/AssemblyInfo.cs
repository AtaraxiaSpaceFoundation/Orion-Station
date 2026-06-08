// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

[assembly: Parallelizable(ParallelScope.Children)]

// Keep the established integration-test limit to avoid the runtime issue tracked in dotnet/runtime#107197.
[assembly: LevelOfParallelism(2)]
