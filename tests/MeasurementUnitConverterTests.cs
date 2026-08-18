using DiwyPOS.PublicSample;
using Xunit;

namespace DiwyPOS.PublicSample.Tests;

public sealed class MeasurementUnitConverterTests
{
    [Fact]
    public void Converts_kilograms_to_grams()
    {
        var result = MeasurementUnitConverter.ToBase(1.7m, MeasurementUnit.Kilogram);
        Assert.Equal(1700m, result.Quantity);
        Assert.Equal(MeasurementUnit.Gram, result.BaseUnit);
    }

    [Fact]
    public void Converts_liters_to_milliliters()
    {
        var result = MeasurementUnitConverter.ToBase(2.5m, MeasurementUnit.Liter);
        Assert.Equal(2500m, result.Quantity);
        Assert.Equal(MeasurementKind.Volume, result.Kind);
    }

    [Fact]
    public void Calculates_cost_per_base_unit()
    {
        var cost = MeasurementUnitConverter.CostPerBaseUnit(40_800m, 1.7m, MeasurementUnit.Kilogram);
        Assert.Equal(24m, cost);
    }

    [Fact]
    public void Rejects_negative_quantity()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() =>
            MeasurementUnitConverter.ToBase(-1m, MeasurementUnit.Gram));
    }
}
