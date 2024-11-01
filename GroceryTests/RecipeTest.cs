namespace GroceryTests;

[TestClass]
public class RecipeTest
{

    [TestMethod]
    public void InitializeTest()
    {
        Recipe recipe = new Recipe("Chicken and Rice", 4);
        string expectedName = "Chicken and Rice";
        Assert.IsTrue(expectedName.Equals(recipe.Name));
    }

    [TestMethod]
    public void SetNameTest()
    {
        Recipe recipe = new Recipe("Chicken and Rice", 4);
        string expectedName = "Chicken and More Rice";
        recipe.Name = "Chicken and More Rice";
        Assert.IsTrue(expectedName.Equals(recipe.Name));
    }

    [TestMethod]
    public void ServingsTest()
    {
        Recipe recipe = new Recipe("Chicken and Rice", 4);
        int expectedServed = 4;
        Assert.AreEqual(expectedServed, recipe.Servings);
    }

    [TestMethod]
    public void AddIngredientsTest()
    {
        Recipe recipe = new Recipe("Chicken and Rice", 4);
        Ingredient ingredientOne = new Ingredient("rice", 1, "cup");
        Ingredient ingredientTwo = new Ingredient("chicken", 1, "cup");
        recipe.Ingredients.Add(ingredientOne);
        recipe.Ingredients.Add(ingredientTwo);
        int expectedIngredients = 2;
        Assert.AreEqual(expectedIngredients, recipe.Ingredients.Count);
    }

    [TestMethod]
    public void ScaleRecipeTest()
    {
        Recipe recipe = new Recipe("Chicken and Rice", 4);
        Ingredient ingredientOne = new Ingredient("rice", 1, "cup");
        Ingredient ingredientTwo = new Ingredient("chicken", 1, "cup");
        recipe.Ingredients.Add(ingredientOne);
        recipe.Ingredients.Add(ingredientTwo);
        recipe.ScaleBy(1.5);
        Assert.AreEqual(1.5, recipe.Ingredients[0].Quantity);
        Assert.AreEqual(1.5, recipe.Ingredients[1].Quantity);
    }
}