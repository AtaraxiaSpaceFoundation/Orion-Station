using System.Linq;
using Content.IntegrationTests;
using Content.IntegrationTests.Fixtures;

namespace Content.Orion.IntegrationTests.Tests.ModuleLoading;

[TestFixture]
public sealed partial class OrionModuleLoadingTest : GameTest
{
    [Test]
    public async Task OrionModuleLoads()
    {
        using (Assert.EnterMultipleScope())
        {
            Assert.That(Pair, Is.Not.Null);
            Assert.That(Server, Is.Not.Null);
            Assert.That(Client, Is.Not.Null);
        }

        var clientAssemblies = PoolManager.GetAssemblies(client: true);
        var serverAssemblies = PoolManager.GetAssemblies(client: false);

        var clientNames = clientAssemblies
            .Select(assembly => assembly.GetName().Name)
            .ToArray();

        var serverNames = serverAssemblies
            .Select(assembly => assembly.GetName().Name)
            .ToArray();

        Assert.That(clientNames, Does.Contain("Content.Orion.Client"));
        Assert.That(clientNames, Does.Contain("Content.Orion.Shared"));
        using (Assert.EnterMultipleScope())
        {
            Assert.That(clientNames, Does.Contain("Content.Orion.Common"));

            Assert.That(serverNames, Does.Contain("Content.Orion.Server"));
        }
        Assert.That(serverNames, Does.Contain("Content.Orion.Shared"));
        Assert.That(serverNames, Does.Contain("Content.Orion.Common"));

        var testMap = await Pair.CreateTestMap();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(testMap, Is.Not.Null);
            Assert.That(TestMap, Is.Not.Null);
        }
    }
}
