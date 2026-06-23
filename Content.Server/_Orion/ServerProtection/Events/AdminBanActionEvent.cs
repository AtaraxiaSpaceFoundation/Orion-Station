using Robust.Shared.Network;

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class AdminBanActionEvent : EntityEventArgs
{
    public AdminBanActionEvent(NetUserId adminUserId, string adminName, string targetName)
    {
        AdminUserId = adminUserId;
        AdminName = adminName;
        TargetName = targetName;
    }

    public NetUserId AdminUserId { get; }
    public string AdminName { get; }
    public string TargetName { get; }
}
