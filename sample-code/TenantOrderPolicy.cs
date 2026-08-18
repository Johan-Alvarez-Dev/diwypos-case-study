namespace DiwyPOS.PublicSample;

public enum PosRole { Waiter, Kitchen, Manager, Administrator }
public enum OrderOperation { Read, Modify, Cancel }

public sealed record PosPrincipal(Guid UserId, Guid TenantId, PosRole Role);

public static class TenantOrderPolicy
{
    public static bool CanAccess(
        PosPrincipal principal, Guid orderTenantId, OrderOperation operation)
    {
        ArgumentNullException.ThrowIfNull(principal);
        if (principal.UserId == Guid.Empty || principal.TenantId == Guid.Empty)
            return false;
        if (principal.TenantId != orderTenantId)
            return false;

        return operation switch
        {
            OrderOperation.Read => true,
            OrderOperation.Modify => principal.Role is not PosRole.Kitchen,
            OrderOperation.Cancel => principal.Role is PosRole.Manager or PosRole.Administrator,
            _ => false
        };
    }
}
