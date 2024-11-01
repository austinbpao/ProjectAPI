using System.Collections.Generic;
using System.Collections;

namespace GroceryClassLib;

public class RecipeEnumerator: IEnumerator<Recipe>
{
    private List<Recipe> _recipes;
    private Recipe? _current;
    private int currentIndex;
    public RecipeEnumerator(List<Recipe> recipeList){
        _recipes = recipeList;
        currentIndex = -1;
        _current = default(Recipe);
    }

    public Recipe Current
    {
        get { return _current ?? new Recipe(); }
    }

    private object Current1
    {
        get {return this.Current; } 
    }

    object IEnumerator.Current
    {
        get {return Current1; }
    }

    public bool MoveNext()
    {
        _current = _recipes[currentIndex];
        if (++currentIndex >= _recipes.Count)
        {
            return false;
        }
        else
        {
            _current = _recipes[currentIndex];
        }
        return true;
    }

    public void Reset()
    {
        currentIndex = -1;
    }

    public void Dispose() {}

}