namespace GroceryClassLib;
public class Ingredient
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Quantity 
    {
        get { return Measurement.Quantity; }
        set { Measurement.Quantity = value; }
    }

    public string Unit
    {
        get { return Measurement.Name; }
        set { Measurement.Name = value; }
    }

    public Measurement Measurement = new Measurement();

    public Ingredient(string name, double quantity, string unit){
        Name = name;
        Quantity = quantity;
        Unit = unit;
    }

    public Ingredient(int id, string name, double quantity, string unit){
        Id = id;
        Name = name;
        Quantity = quantity;
        Unit = unit;
    }

    public Ingredient(){
        Name = "";
    }

    public Ingredient(int id, string name){
        Id = id;
        Name = name;
    }
    
}
