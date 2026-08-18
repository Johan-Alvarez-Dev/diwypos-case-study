using DiwyPOS.PublicSample;
using Xunit;

namespace DiwyPOS.PublicSample.Tests;

public sealed class TenantOrderPolicyTests
{
    [Fact]
    public void Denies_cross_tenant_access_even_for_an_administrator()
    {
        var principal = new PosPrincipal(Guid.NewGuid(), Guid.NewGuid(), PosRole.Administrator);
        Assert.False(TenantOrderPolicy.CanAccess(principal, Guid.NewGuid(), OrderOperation.Read));
    }

    [Fact]
    public void Only_management_roles_can_cancel_orders()
    {
        var tenantId = Guid.NewGuid();
        var waiter = new PosPrincipal(Guid.NewGuid(), tenantId, PosRole.Waiter);
        var manager = new PosPrincipal(Guid.NewGuid(), tenantId, PosRole.Manager);

        Assert.False(TenantOrderPolicy.CanAccess(waiter, tenantId, OrderOperation.Cancel));
        Assert.True(TenantOrderPolicy.CanAccess(manager, tenantId, OrderOperation.Cancel));
    }
}
