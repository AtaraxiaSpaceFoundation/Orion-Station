namespace Content.Server._Orion.ServerProtection.Events;

public sealed class EmoteChatMessageDetectedEvent : EntityEventArgs
{
    public EmoteChatMessageDetectedEvent(EntityUid source, string action, bool voluntary)
    {
        Source = source;
        Action = action;
        Voluntary = voluntary;
    }

    public EntityUid Source { get; }
    public string Action { get; }
    public bool Voluntary { get; }
}
