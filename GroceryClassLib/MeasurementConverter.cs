namespace GroceryClassLib;

public static class MeasurementConverter
{

    private static Dictionary<string, Dictionary<string, double>> _conversions = new Dictionary<string, Dictionary<string, double>>()

    {
        {"Cup", new Dictionary<string, double>{{"Tablespoon", 16}, {"Ounce", 8}}},
        {"Tablespoon", new Dictionary<string, double>{{"Cup", .0625}, {"Ounce", .5}}},
        {"Ounce", new Dictionary<string, double>{{"Tablespoon", 2}, {"Cup", .125}}}
    };
    public static Measurement Convert(Measurement measurement, string newUnit)
    {
        double newAmount = _conversions[measurement.Name][newUnit] * measurement.Quantity;
        return new Measurement(newUnit, newAmount);
    }
}