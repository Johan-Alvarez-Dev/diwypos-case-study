namespace DiwyPOS.PublicSample;

public enum MeasurementKind { Mass, Volume, Count }
public enum MeasurementUnit { Milligram, Gram, Kilogram, Ounce, Pound, Milliliter, Liter, Unit }

public readonly record struct BaseMeasurement(
    decimal Quantity,
    MeasurementKind Kind,
    MeasurementUnit BaseUnit);

public static class MeasurementUnitConverter
{
    public static BaseMeasurement ToBase(decimal quantity, MeasurementUnit unit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);

        return unit switch
        {
            MeasurementUnit.Milligram => new(quantity / 1000m, MeasurementKind.Mass, MeasurementUnit.Gram),
            MeasurementUnit.Gram => new(quantity, MeasurementKind.Mass, MeasurementUnit.Gram),
            MeasurementUnit.Kilogram => new(quantity * 1000m, MeasurementKind.Mass, MeasurementUnit.Gram),
            MeasurementUnit.Ounce => new(quantity * 28.349523125m, MeasurementKind.Mass, MeasurementUnit.Gram),
            MeasurementUnit.Pound => new(quantity * 453.59237m, MeasurementKind.Mass, MeasurementUnit.Gram),
            MeasurementUnit.Milliliter => new(quantity, MeasurementKind.Volume, MeasurementUnit.Milliliter),
            MeasurementUnit.Liter => new(quantity * 1000m, MeasurementKind.Volume, MeasurementUnit.Milliliter),
            MeasurementUnit.Unit => new(quantity, MeasurementKind.Count, MeasurementUnit.Unit),
            _ => throw new ArgumentOutOfRangeException(nameof(unit))
        };
    }

    public static decimal CostPerBaseUnit(decimal cost, decimal purchasedQuantity, MeasurementUnit unit)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(cost);
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(purchasedQuantity);

        var measurement = ToBase(purchasedQuantity, unit);
        return cost / measurement.Quantity;
    }
}
