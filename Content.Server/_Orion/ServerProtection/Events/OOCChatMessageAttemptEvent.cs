using Robust.Shared.Player;

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class OOCChatMessageAttemptEvent : CancellableEntityEventArgs
{
    public OOCChatMessageAttemptEvent(string message, ICommonSession player)
    {
        Message = message;
        Player = player;
    }

    public string Message { get; }
    public ICommonSession Player { get; }
}
