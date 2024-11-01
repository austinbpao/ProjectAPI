namespace GroceryClassLib;

public class Measurement {
    public int Id { get; set; }
    public string Name { get; set; } = "";

    public double Quantity { get; set; } = 0;

    public Measurement()
    {}

    public Measurement(int id, string name, double quantity)
    {
        Id = id;
        Name = name;
        Quantity = quantity;
    }
    public Measurement(string name, double quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}