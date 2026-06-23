using Robust.Shared.Network;

namespace Content.Server._Orion.ServerProtection.Events;

public sealed class AdminPermissionRemovalActionEvent : EntityEventArgs
{
    public AdminPermissionRemovalActionEvent(NetUserId adminUserId, string adminName, string targetName, string action)
    {
        AdminUserId = adminUserId;
        AdminName = adminName;
        TargetName = targetName;
        Action = action;
    }

    public NetUserId AdminUserId { get; }
    public string AdminName { get; }
    public string TargetName { get; }
    public string Action { get; }
}
