namespace GroceryControllers.Controllers;

public class OrderResponse<T> {
    public string statusCode { get; set; } = "200";
    public string message { get; set; } = "";
    public T? Object { get; set; }
}