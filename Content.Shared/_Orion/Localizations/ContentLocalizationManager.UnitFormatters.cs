// ReSharper disable once CheckNamespace
namespace Content.Shared.Localizations
{
    public sealed partial class ContentLocalizationManager
    {
        private static ILocValue FormatUnitsGeneric(LocArgs args, string mode, Func<double, double>? transformValue = null)
        {
            const int maxPlaces = 5;
            var value = ((LocValueNumber) args.Args[0]).Value;

            if (transformValue != null)
                value = transformValue(value);

            var places = 0;
            while (value > 1000 && places < maxPlaces)
            {
                value /= 1000;
                places++;
            }

            return new LocValueString(Loc.GetString(mode, ("divided", value), ("places", places)));
        }

        private static ILocValue FormatPressure(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-pressure");
        }

        private static ILocValue FormatPowerWatts(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-power-watts");
        }

        private static ILocValue FormatPowerJoules(LocArgs args)
        {
            return FormatUnitsGeneric(args, "zzzz-fmt-power-joules");
        }

        private static ILocValue FormatEnergyWattHours(LocArgs args)
        {
            const double joulesToWattHours = 1.0 / 3600;
            return FormatUnitsGeneric(args, "zzzz-fmt-energy-watt-hours", joules => joules * joulesToWattHours);
        }

        private static ILocValue FormatUnits(LocArgs args)
        {
            if (!Units.Types.TryGetValue(((LocValueString) args.Args[0]).Value, out var unitType))
                throw new ArgumentException($"Unknown unit type {((LocValueString) args.Args[0]).Value}");

            var format = ((LocValueString) args.Args[1]).Value;
            var max = double.NegativeInfinity;
            var inputArgs = new double[args.Args.Count - 1];

            for (var i = 2; i < args.Args.Count; i++)
            {
                var value = ((LocValueNumber) args.Args[i]).Value;
                if (value > max)
                    max = value;

                inputArgs[i - 2] = value;
            }

            if (!unitType.TryGetUnit(max, out var unit))
                throw new ArgumentException("Unit out of range for type");

            var formatArgs = new object[inputArgs.Length];
            for (var i = 0; i < inputArgs.Length; i++)
            {
                formatArgs[i] = inputArgs[i] * unit.Factor;
            }

            formatArgs[^1] = Loc.GetString($"units-{unit.Unit.ToLower()}");

            var result = string.Format(
                format.Replace("{UNIT", "{" + $"{formatArgs.Length - 1}"),
                formatArgs);

            return new LocValueString(result);
        }

        private static ILocValue FormatPlaytime(LocArgs args)
        {
            var time = TimeSpan.Zero;
            if (args.Args is { Count: > 0 } && args.Args[0].Value is TimeSpan timeArg)
                time = timeArg;

            return new LocValueString(FormatPlaytime(time));
        }
    }
}
