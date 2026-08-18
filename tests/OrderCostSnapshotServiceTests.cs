using DiwyPOS.PublicSample;
using Xunit;

namespace DiwyPOS.PublicSample.Tests;

public sealed class OrderCostSnapshotServiceTests
{
    [Fact]
    public void Freezes_recipe_cost_for_the_sold_quantity()
    {
        RecipeCostLine[] recipe =
        [
            new("Coffee", 18m, 0.08m),
            new("Milk", 200m, 0.004m)
        ];

        var snapshot = new OrderCostSnapshotService().Create(recipe, 3);

        Assert.Equal(2.24m, snapshot.UnitCost);
        Assert.Equal(6.72m, snapshot.TotalCost);
    }
}
