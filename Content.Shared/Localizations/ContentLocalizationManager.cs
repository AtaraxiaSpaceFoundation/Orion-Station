// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Globalization;
using System.Linq;
using Robust.Shared;
using Robust.Shared.Configuration;
using Robust.Shared.ContentPack;
using Robust.Shared.Utility;

namespace Content.Shared.Localizations
{
    public sealed partial class ContentLocalizationManager
    {
        [Dependency] private ILocalizationManager _loc = default!;
        // Orion-Start
        [Dependency] private ILogManager _logManager = default!;
        [Dependency] private IResourceManager _resourceManager = default!;
        [Dependency] private IConfigurationManager _cfg = default!;
        // Orion-End

        private const string DefaultCultureName = "ru-RU"; // Orion
        private const string FallbackCultureName = "en-US"; // Orion-Edit: Renamed Culture

        // Orion-Start
        private static readonly ResPath LocaleDirectory = new("/Locale");
        private static readonly HashSet<string> SupportedCultureNames = new(StringComparer.OrdinalIgnoreCase)
        {
            DefaultCultureName,
            FallbackCultureName,
        };
        private readonly HashSet<string> _functionsRegistered = new();
        private ISawmill _log = default!;

        public event Action<CultureInfo>? CultureChanged;
        // Orion-End

        /// <summary>
        /// Custom format strings used for parsing and displaying minutes:seconds timespans.
        /// </summary>
        public static readonly string[] TimeSpanMinutesFormats = new[]
        {
            @"m\:ss",
            @"mm\:ss",
            @"%m",
            @"mm",
        };

        // Orion-Start
        public IReadOnlyList<CultureInfo> FoundCultures
        {
            get
            {
                var result = new List<CultureInfo>();
                var added = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                foreach (var entry in _resourceManager.ContentGetDirectoryEntries(LocaleDirectory))
                {
                    var cultureName = entry.TrimEnd('/');
                    if (!SupportedCultureNames.Contains(cultureName))
                        continue;

                    try
                    {
                        var culture = CultureInfo.GetCultureInfo(cultureName, predefinedOnly: false);
                        if (added.Add(culture.Name))
                            result.Add(culture);
                    }
                    catch (Exception)
                    {
                        // Entries under /Locale are not guaranteed to be valid culture identifiers.
                    }
                }

                return result;
            }
        }
        // Orion-End

        // Orion-Edit-Start
        public void Initialize()
        {
            _log = _logManager.GetSawmill("content-loc");
            _cfg.OverrideDefault(CVars.LocCultureName, DefaultCultureName);

            var found = FoundCultures;
            if (found.Count == 0)
            {
                _log.Error("No localization cultures were found under /Locale.");
                return;
            }

            var configuredName = _cfg.GetCVar(CVars.LocCultureName);
            var configured = TryGetFoundCulture(configuredName, found);
            var fallback = TryGetFoundCulture(FallbackCultureName, found);

            if (fallback == null)
            {
                fallback = found[0];
                _log.Error("The fallback culture {FallbackCulture} was not found. Falling back to {FoundCulture}.", FallbackCultureName, fallback.Name);
            }

            TrySetCulture(configured ?? fallback);
        }

        public bool TrySetCulture(CultureInfo culture)
        {
            var found = FoundCultures;
            var selected = TryGetFoundCulture(culture.Name, found);
            if (selected == null)
                return false;

            var fallback = TryGetFoundCulture(FallbackCultureName, found) ?? found.FirstOrDefault();
            if (fallback == null)
                return false;

            try
            {
                EnsureCultureLoaded(fallback);
                _loc.SetFallbackCluture(fallback);
                EnsureCultureLoaded(selected);
                _cfg.SetCVar(CVars.LocCultureName, selected.Name);
                _loc.SetCulture(selected);
                _loc.ReloadLocalizations();
            }
            catch (Exception e)
            {
                _log.Error("Failed to switch localization culture to {Culture}: {Exception}", selected.Name, e);
                try
                {
                    EnsureCultureLoaded(fallback);
                    _loc.SetFallbackCluture(fallback);
                    _loc.SetCulture(fallback);
                    _cfg.SetCVar(CVars.LocCultureName, fallback.Name);
                    _loc.ReloadLocalizations();
                }
                catch (Exception fallbackException)
                {
                    _log.Error("Failed to restore fallback localization culture {Culture}: {Exception}", fallback.Name, fallbackException);
                }

                return false;
            }

            NotifyCultureChanged(selected);
            return true;
        }

        public bool TrySetCulture(string cultureName)
        {
            try
            {
                return TrySetCulture(CultureInfo.GetCultureInfo(cultureName, predefinedOnly: false));
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static CultureInfo? TryGetFoundCulture(string cultureName, IReadOnlyList<CultureInfo> found)
        {
            CultureInfo requested;
            try
            {
                requested = CultureInfo.GetCultureInfo(cultureName, predefinedOnly: false);
            }
            catch (Exception)
            {
                return null;
            }

            return found.FirstOrDefault(culture => culture.NameEquals(requested));
        }

        private void NotifyCultureChanged(CultureInfo culture)
        {
            try
            {
                CultureChanged?.Invoke(culture);
            }
            catch (Exception e)
            {
                _log.Error("A culture change handler failed for {Culture}: {Exception}", culture.Name, e);
            }
        }

        private void EnsureCultureLoaded(CultureInfo culture)
        {
            if (!_loc.HasCulture(culture))
                _loc.LoadCulture(culture);

            if (_functionsRegistered.Contains(culture.Name))
                return;

            _loc.AddFunction(culture, "PRESSURE", FormatPressure);
            _loc.AddFunction(culture, "POWERWATTS", FormatPowerWatts);
            _loc.AddFunction(culture, "POWERJOULES", FormatPowerJoules);
            _loc.AddFunction(culture, "ENERGYWATTHOURS", FormatEnergyWattHours);
            _loc.AddFunction(culture, "UNITS", FormatUnits);
            _loc.AddFunction(culture, "TOSTRING", args => FormatToString(culture, args));
            _loc.AddFunction(culture, "LOC", FormatLoc);
            _loc.AddFunction(culture, "NATURALFIXED", args => FormatNaturalFixed(culture, args));
            _loc.AddFunction(culture, "NATURALPERCENT", args => FormatNaturalPercent(culture, args));
            _loc.AddFunction(culture, "PLAYTIME", FormatPlaytime);

            if (culture.NameEquals(CultureInfo.GetCultureInfo(FallbackCultureName)))
            {
                _loc.AddFunction(culture, "MAKEPLURAL", FormatMakePlural);
                _loc.AddFunction(culture, "MANY", FormatMany);
            }

            _functionsRegistered.Add(culture.Name);
        }
    }
    // Orion-Edit-End
}
