namespace GroceryTests;

[TestClass]
public class IngredientTest
{
    [TestMethod]
    public void InitializeTest()
    {
        Ingredient ingredient = new Ingredient("Rice", 10, "Cup");
        string expectedName = "Rice";
        double expectedQuantity = 10;
        Assert.IsTrue(expectedName.Equals(ingredient.Name));
        Assert.IsTrue(expectedQuantity.Equals(ingredient.Quantity));
    }

    [TestMethod]
    public void ConvertToTblsTest()
    {
        Ingredient ingredient = new Ingredient("Rice", 1, "Cup");
        Measurement convertedMeasure = MeasurementConverter.Convert(ingredient.Measurement, "Tablespoon");
        Assert.AreEqual(16, convertedMeasure.Quantity);
        Assert.AreEqual("Tablespoon",convertedMeasure.Name);
    }

    [TestMethod]
    public void ConvertToTblsAndBackTest()
    {
        Ingredient ingredient = new Ingredient("Rice", 1, "Cup");
        ingredient.Measurement = MeasurementConverter.Convert(ingredient.Measurement, "Tablespoon");
        ingredient.Measurement = MeasurementConverter.Convert(ingredient.Measurement, "Cup");
        Assert.AreEqual(1, ingredient.Quantity);
        Assert.AreEqual("Cup",ingredient.Unit);
    }

    [TestMethod]
    public void ConvertToOzFromCup()
    {
        Ingredient ingredient = new Ingredient("Rice", 4, "Ounce");
        ingredient.Measurement = MeasurementConverter.Convert(ingredient.Measurement,"Cup");
        Assert.AreEqual(.5, ingredient.Quantity);
        Assert.AreEqual("Cup",ingredient.Unit);
    }

    [TestMethod]
    public void ConvertAroundTheWorld()
    {
        Ingredient ingredient = new Ingredient("Rice", 4, "Ounce");
        double expectedQuantity = 4;
        string expectedUnit = "Ounce";
        ingredient.Measurement = MeasurementConverter.Convert(ingredient.Measurement,"Cup");
        ingredient.Measurement = MeasurementConverter.Convert(ingredient.Measurement,"Tablespoon");
        ingredient.Measurement = MeasurementConverter.Convert(ingredient.Measurement,"Ounce");
        Assert.AreEqual(expectedQuantity, ingredient.Quantity);
        Assert.AreEqual(expectedUnit, ingredient.Unit);

    }

}