using Content.IntegrationTests.Fixtures;
using Content.Shared.Localizations;
using Robust.Shared.Localization;

namespace Content.Orion.IntegrationTests.Tests.Localization;

public sealed class ContentCultureSwitchTest : GameTest
{
    [Test]
    public async Task SwitchingCultureChangesLocalization()
    {
        var client = Client;

        await client.WaitAssertion(() =>
        {
            var contentLoc = client.ResolveDependency<ContentLocalizationManager>();
            var loc = client.ResolveDependency<ILocalizationManager>();
            var original = loc.DefaultCulture!;

            try
            {
                using (Assert.EnterMultipleScope())
                {
                    Assert.That(contentLoc.TrySetCulture("en-US"), Is.True);
                    Assert.That(loc.GetString("culture-switch-test-value"), Is.EqualTo("English value"));

                    Assert.That(contentLoc.TrySetCulture("ru-RU"), Is.True);
                }
                Assert.That(loc.GetString("culture-switch-test-value"), Is.EqualTo("Русское значение"));
            }
            finally
            {
                contentLoc.TrySetCulture(original);
            }
        });
    }
}
