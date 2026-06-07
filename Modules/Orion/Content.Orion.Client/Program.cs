using System;
using Robust.Client;

namespace Content.Orion.Client;

internal static class Program
{
    [STAThread]
    public static void Main(string[] args)
    {
        ContentStart.Start(args);
    }
}
