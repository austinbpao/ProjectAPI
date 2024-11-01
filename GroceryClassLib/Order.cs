namespace GroceryClassLib;

public class DrinkOrder {
    public int Id { get; set; }
    public Recipe Drink { get; set; } = new Recipe();
    public string CustomerName { get; set; } = "";
    public string Instructions { get; set; } = "";
    public bool Complete { get; set; } = false;
    public DrinkOrder(int id, string customer, Recipe drink, string instructions, bool complete)
    {
        Id = id;
        CustomerName = customer;
        Drink = drink;
        Instructions = instructions;
        Complete = complete;
    }
    public DrinkOrder(string customer, Recipe drink, string instructions, bool complete)
    {
        CustomerName = customer;
        Drink = drink;
        Instructions = instructions;
        Complete = complete;
    }
    public DrinkOrder(int id, string customer, string instructions, bool complete)
    {
        Id = id;
        CustomerName = customer;
        Instructions = instructions;
        Complete = complete;
    }
    public DrinkOrder(){}
}

