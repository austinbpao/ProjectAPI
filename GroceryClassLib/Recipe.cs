namespace GroceryClassLib;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Servings { get; set; } = 0;
    public string Category { get; set; } = "";
    public string Subcategory { get; set; } = "";
    public TimeSpan CookTime { get; set; }
    public TimeSpan PrepTime { get; set; }
    public TimeSpan TotalTime
    {
        get { return PrepTime.Add(CookTime); }
    }
    public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

    public Recipe(string name, int servings)
    {
        Name = name;
        Servings = servings;
    }

    public Recipe(int id, string name, int servings)
    {
        Id = id;
        Name = name;
        Servings = servings;
    }
        public Recipe(int id, string name, int servings, string category, string subcategory)
    {
        Id = id;
        Name = name;
        Servings = servings;
        Category = category;
        Subcategory = subcategory;
    }
    public Recipe() {}

    public void ScaleBy(double ratio)
    {
        int newServings = Convert.ToInt16(Math.Truncate((double)this.Servings * ratio));
        double trueRatio = (double)newServings / (double)this.Servings;
        this.Servings = newServings;
        
        foreach(Ingredient ingredient in this.Ingredients)
        {
            ingredient.Quantity = ingredient.Quantity * trueRatio;
        }
    }
}