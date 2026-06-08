// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using Content.IntegrationTests.Fixtures;

namespace Content.Orion.IntegrationTests;

[TestFixture]
public sealed partial class OrionTest : GameTest
{
    [Test]
    public Task TestLoadAll()
    {
        Server.Log.Debug("Server: If you see this, test did launch!");
        Client.Log.Debug("Client: If you see this, test did launch!");
        return Task.CompletedTask;
    }
}
