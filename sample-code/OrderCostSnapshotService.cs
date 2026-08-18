namespace DiwyPOS.PublicSample;

public sealed record RecipeCostLine(
    string Ingredient,
    decimal BaseQuantity,
    decimal CostPerBaseUnit);

public sealed record OrderCostSnapshot(
    decimal UnitCost,
    int Quantity,
    decimal TotalCost);

public interface IOrderCostSnapshotService
{
    OrderCostSnapshot Create(IEnumerable<RecipeCostLine> recipe, int quantity);
}

public sealed class OrderCostSnapshotService : IOrderCostSnapshotService
{
    public OrderCostSnapshot Create(IEnumerable<RecipeCostLine> recipe, int quantity)
    {
        ArgumentNullException.ThrowIfNull(recipe);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(quantity);

        var unitCost = recipe.Sum(line =>
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(line.Ingredient);
            if (line.BaseQuantity < 0 || line.CostPerBaseUnit < 0)
                throw new ArgumentOutOfRangeException(nameof(recipe), "Recipe values cannot be negative.");
            return line.BaseQuantity * line.CostPerBaseUnit;
        });

        unitCost = decimal.Round(unitCost, 2, MidpointRounding.AwayFromZero);
        return new OrderCostSnapshot(unitCost, quantity, unitCost * quantity);
    }
}
