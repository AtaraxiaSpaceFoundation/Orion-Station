// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.Benchmarks;
using Content.IntegrationTests;

namespace Content.Orion.IntegrationTests;

[SetUpFixture]
public sealed class OrionPoolManagerTestEventHandler
{
    [OneTimeSetUp]
    public void Setup()
    {
        IntegrationTestHelpers.ChangeRootDir("../../../");
        PoolManagerHelpers.Setup(typeof(OrionPoolManagerTestEventHandler).Assembly);
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        PoolManager.Shutdown();
    }
}
