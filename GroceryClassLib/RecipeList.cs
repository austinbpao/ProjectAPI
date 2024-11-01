using System.Collections.Generic;
using System.Collections;

namespace GroceryClassLib;

public class RecipeList : IEnumerable<Recipe>
{
    private List<Recipe> _recipes;
    private Recipe _current = new Recipe();
    public RecipeList(List<Recipe> rList)
    {
        _recipes = rList;
    }

    public RecipeList()
    {
        _recipes = new List<Recipe>();
    }

    public IEnumerator<Recipe> GetEnumerator()
    {
        return new RecipeEnumerator(_recipes);
    }

    private IEnumerator GetEnumerator1()
    {
        return this.GetEnumerator();
    }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator1();
    }
}