// SPDX-FileCopyrightText: 2026 PuroSlavKing <puroslavking@yahoo.com>
//
// SPDX-License-Identifier: AGPL-3.0-or-later

using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Content.Shared.Localizations
{
    public sealed partial class ContentLocalizationManager
    {
        private static ILocValue FormatMany(LocArgs args)
        {
            var count = ((LocValueNumber) args.Args[1]).Value;
            if (Math.Abs(count - 1) < 0.0001f)
                return (LocValueString) args.Args[0];

            return (LocValueString) FormatMakePlural(args);
        }

        private static ILocValue FormatNaturalPercent(CultureInfo culture, LocArgs args)
        {
            var number = ((LocValueNumber) args.Args[0]).Value * 100;
            var maxDecimals = (int) Math.Floor(((LocValueNumber) args.Args[1]).Value);
            var formatter = (NumberFormatInfo) NumberFormatInfo.GetInstance(culture).Clone();
            formatter.NumberDecimalDigits = maxDecimals;
            return new LocValueString(string.Format(formatter, "{0:N}", number)
                .TrimEnd('0')
                .TrimEnd(char.Parse(formatter.NumberDecimalSeparator)) + "%");
        }

        private static ILocValue FormatNaturalFixed(CultureInfo culture, LocArgs args)
        {
            var number = ((LocValueNumber) args.Args[0]).Value;
            var maxDecimals = (int) Math.Floor(((LocValueNumber) args.Args[1]).Value);
            var formatter = (NumberFormatInfo) NumberFormatInfo.GetInstance(culture).Clone();
            formatter.NumberDecimalDigits = maxDecimals;
            return new LocValueString(string.Format(formatter, "{0:N}", number)
                .TrimEnd('0')
                .TrimEnd(char.Parse(formatter.NumberDecimalSeparator)));
        }

        private static readonly Regex PluralEsRule = new("^.*(s|sh|ch|x|z)$");

        private static ILocValue FormatMakePlural(LocArgs args)
        {
            var text = ((LocValueString) args.Args[0]).Value;
            var split = text.Split(" ", 1);
            var firstWord = split[0];
            var plural = PluralEsRule.IsMatch(firstWord) ? $"{firstWord}es" : $"{firstWord}s";
            return new LocValueString(split.Length == 1
                ? plural
                : $"{plural} {split[1]}");
        }

        public static string FormatList(List<string> list)
        {
            return list.Count switch
            {
                <= 0 => string.Empty,
                1 => list[0],
                2 => $"{list[0]} and {list[1]}",
                _ => $"{string.Join(", ", list.GetRange(0, list.Count - 1))}, and {list[^1]}",
            };
        }

        public static string FormatListToOr(List<string> list)
        {
            return list.Count switch
            {
                <= 0 => string.Empty,
                1 => list[0],
                2 => $"{list[0]} or {list[1]}",
                _ => $"{string.Join(", ", list.GetRange(0, list.Count - 1))}, or {list[^1]}",
            };
        }

        public static string FormatDirection(Direction dir)
        {
            return Loc.GetString($"zzzz-fmt-direction-{dir}");
        }

        public static string FormatPlaytime(TimeSpan time)
        {
            time = TimeSpan.FromMinutes(Math.Ceiling(time.TotalMinutes));
            return Loc.GetString("zzzz-fmt-playtime", ("hours", (int) time.TotalHours), ("minutes", time.Minutes));
        }

        private static ILocValue FormatLoc(LocArgs args)
        {
            var id = ((LocValueString) args.Args[0]).Value;
            return new LocValueString(Loc.GetString(id,
                args.Options.Select(x => (x.Key, x.Value.Value!)).ToArray()));
        }

        private static ILocValue FormatToString(CultureInfo culture, LocArgs args)
        {
            var arg = args.Args[0];
            var fmt = ((LocValueString) args.Args[1]).Value;
            var obj = arg.Value;
            if (obj is IFormattable formattable)
                return new LocValueString(formattable.ToString(fmt, culture));

            return new LocValueString(obj?.ToString() ?? "");
        }
    }
}
