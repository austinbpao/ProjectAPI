using Microsoft.AspNetCore.Mvc;
using GroceryClassLib;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Security;
using GroceryControllers.Controllers;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Data;
using SqlUtils;


namespace GroceryControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly ILogger<DrinkController> _logger;

    public OrderController(ILogger<DrinkController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public OrderResponse<List<DrinkOrder>> GetOrderList(){
        OrderResponse<List<DrinkOrder>> response = new OrderResponse<List<DrinkOrder>>();
        try
        {
            var appsettings = System.Configuration.ConfigurationManager.AppSettings;
            List<DrinkOrder> orderList = new List<DrinkOrder>();
            string sql = @"EXEC GetDrinkOrderList";
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                DataSet resultSet = SqlUtils.SqlUtils.GetDataSet(conn, sql);
                response.statusCode = "200";
                response.message = "Okay";
                response.Object = UnpackOrderResponse(resultSet);
            }
        }
        catch (Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }

    [HttpPut]
    public OrderResponse<List<DrinkOrder>> PlaceOrder([FromBody]DrinkOrder drinkOrder){
        OrderResponse<List<DrinkOrder>> response = new OrderResponse<List<DrinkOrder>>();
        try
        {
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                List<string> dtColumns = new List<string>() {"Id", "RecipeId", "CustomerName", "Instructions", "Complete", "Category"};
                List<object> queryValues = new List<object>() {drinkOrder.Id, drinkOrder.Drink.Id, drinkOrder.CustomerName, drinkOrder.Instructions, Convert.ToInt16(drinkOrder.Complete)};
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<DrinkOrder>(conn, dtColumns, queryValues, "AddDrinkOrders", "@DrinkOrderTable");
                response.Object = UnpackOrderResponse(returnSet);
            }
            response.statusCode = "200";
            response.message = "Okay";

        }
        catch (Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }

    [HttpPatch]
    public OrderResponse<List<DrinkOrder>> UpdateOrder([FromBody]DrinkOrder drinkOrder)
    {
        OrderResponse<List<DrinkOrder>> response = new OrderResponse<List<DrinkOrder>>();
        try
        {
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                List<string> dtColumns = new List<string>() {"Id", "RecipeId", "CustomerName", "Instructions", "Complete", "Category"};
                List<object> queryValues = new List<object>() {drinkOrder.Id, drinkOrder.Drink.Id, drinkOrder.CustomerName, drinkOrder.Instructions, Convert.ToInt16(drinkOrder.Complete)};
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<DrinkOrder>(conn, dtColumns, queryValues, "UpdateDrinkOrders", "@DrinkOrderTable");
                response.Object = UnpackOrderResponse(returnSet);
            }
            response.statusCode = "200";
            response.message = "Okay";
        }
        catch(Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }

    [HttpDelete]
    public void DeleteOrder([FromBody]DrinkOrder drinkOrder)
    {
        OrderResponse<List<DrinkOrder>> response = new OrderResponse<List<DrinkOrder>>();
        try
        {
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                List<string> dtColumns = new List<string>() {"Id", "RecipeId", "CustomerName", "Instructions", "Complete", "Category"};
                List<object> queryValues = new List<object>() {drinkOrder.Id, drinkOrder.Drink.Id, drinkOrder.CustomerName, drinkOrder.Instructions, Convert.ToInt16(drinkOrder.Complete)};
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<DrinkOrder>(conn, dtColumns, queryValues, "DeleteDrinkOrder", "@DrinkOrderTable");
            }
            response.statusCode = "200";
            response.message = "Okay";
        }
        catch(Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
    }

    public static List<DrinkOrder> UnpackOrderResponse(DataSet orderData)
    {

        List<DrinkOrder> drinkOrders = new List<DrinkOrder>();
        foreach(DataRow row in orderData.Tables[0].Rows)
        {
            Recipe drink = new Recipe();
            drink.Id = (int)row["RecipeId"]; 
            drinkOrders.Add(new DrinkOrder((int)row["Id"], row["CustomerName"].ToString() ?? "", drink, row["Instructions"].ToString() ?? "", Convert.ToBoolean(row["Complete"])));
        }
        foreach(DrinkOrder drinkOrder in drinkOrders)
        {

            drinkOrder.Drink = orderData.Tables[1].AsEnumerable().Where(i => (int)i["Id"] == drinkOrder.Drink.Id)
            .Select(i => new Recipe((int)i["Id"],i["Name"].ToString()!,(int)i["Servings"], i["Category"].ToString() ?? "", i["SubCategory"].ToString() ?? "")).First();  //Refactor needed here to make orders have a list if drinks
            drinkOrder.Drink.Ingredients.AddRange(orderData.Tables[2].AsEnumerable()
            .Where(i => (int)i["RecipeId"] == drinkOrder.Drink.Id)
            .Select(i => new Ingredient((int)i["IngredientId"], i["IngredientName"].ToString() ?? "", Decimal.ToDouble((decimal)i["Quantity"]), i["UnitName"].ToString() ?? "")));
        }
        return drinkOrders;
    }
}