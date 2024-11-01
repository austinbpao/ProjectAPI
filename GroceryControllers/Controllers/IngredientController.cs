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
public class IngredientController : ControllerBase
{
    private readonly ILogger<IngredientController> _logger;

    public IngredientController(ILogger<IngredientController> logger)
    {
        _logger = logger;
    }

    [HttpGet] //Done, untested
    public OrderResponse<List<Ingredient>> GetIngredientList(){
        OrderResponse<List<Ingredient>> response = new OrderResponse<List<Ingredient>>();
        try
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                string sql = "EXEC GetIngredients";
                conn.Open();
                DataSet returnSet = SqlUtils.SqlUtils.GetDataSet(conn, sql);
                foreach(DataRow row in returnSet.Tables[0].Rows)
                {
                    Int32.TryParse(row["ID"].ToString(), out int id);
                    Double.TryParse(row["QuantityInStock"].ToString(), out double quantity);
                    ingredientList.Add(new Ingredient(id, row["Name"].ToString()!, quantity, "Ounce"));
                }
                response.statusCode = "200";
                response.message = "Okay";
                response.Object = ingredientList;
            }
        }
        catch (Exception ex){
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }

    [HttpPut] //Done, untested
    public OrderResponse<List<Ingredient>> AddIngredients([FromBody] Ingredient ingredient){
        OrderResponse<List<Ingredient>> response = new OrderResponse<List<Ingredient>>();
        try 
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                conn.Open();
                List<string> dtColumns = new List<string>() {"Id", "Name", "Quantity"};
                List<object> queryValues = new List<object>() {ingredient.Id, ingredient.Name, ingredient.Quantity};
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<Ingredient>(conn, dtColumns, queryValues, "AddIngredients", "@IngredientTable");
                foreach(DataRow row in returnSet.Tables[0].Rows)
                {
                    Int32.TryParse(row["ID"].ToString(), out int id);
                    Double.TryParse(row["QuantityInStock"].ToString(), out double quantity);
                    ingredientList.Add(new Ingredient(id, row["Name"].ToString()!, quantity, "Ounce"));
                }
                response.Object=ingredientList;
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

    [HttpPost] //Done, untested
    public OrderResponse<List<Ingredient>> GetIngredientListByIds([FromBody] IEnumerable<int> ids){
        OrderResponse<List<Ingredient>> response = new OrderResponse<List<Ingredient>>();
        try 
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            DataTable idDataTable = new DataTable();
            idDataTable.Columns.Add();
            foreach(int id in ids)
            {
                idDataTable.Rows.Add(id);
            }
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                conn.Open();
                DataSet returnSet = SqlUtils.SqlUtils.GetByIdTable(conn, idDataTable, @"GetIngredientsByID");
                foreach(DataRow row in returnSet.Tables[0].Rows)
                {
                    Int32.TryParse(row["ID"].ToString(), out int id);
                    Double.TryParse(row["QuantityInStock"].ToString(), out double quantity);
                    ingredientList.Add(new Ingredient(id, row["Name"].ToString()!, quantity, "Ounce"));
                }
                response.statusCode = "200";
                response.message = "Okay";
                response.Object=ingredientList;
            }
        }
        catch (Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }
    
    [HttpPatch] //Done, untested
    public OrderResponse<List<Ingredient>> UpdateIngredients([FromBody] Ingredient ingredient)
    {
        OrderResponse<List<Ingredient>> response = new OrderResponse<List<Ingredient>>();
        try
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                List<string> dtColumns = new List<string>() { "Id", "Name" };
                List<object> queryValues = new List<object>() { ingredient.Id, ingredient.Name, ingredient.Quantity };
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<Object>(conn, dtColumns, queryValues, "UpdateObjects", "@ObjectTable");
                foreach(DataRow row in returnSet.Tables[0].Rows)
                {
                    Int32.TryParse(row["ID"].ToString(), out int id);
                    Double.TryParse(row["QuantityInStock"].ToString(), out double quantity);
                    ingredientList.Add(new Ingredient(id, row["Name"].ToString()!, quantity, "Ounce"));
                }
                response.Object = ingredientList;
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

    [HttpDelete] //Done, Untested
    public OrderResponse<List<Ingredient>> DeleteIngredients([FromBody] Ingredient ingredient)
    {
        OrderResponse<List<Ingredient>> response = new OrderResponse<List<Ingredient>>();
        try
        {
            List<Ingredient> ingredientList = new List<Ingredient>();
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                conn.Open();
                List<string> dtColumns = new List<string>() { "Id", "RecipeId" };
                List<object> queryValues = new List<object>() { ingredient.Id, ingredient.Name, ingredient.Quantity };
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<Ingredient>(conn, dtColumns, queryValues, "AddIngredients", "@IngredientTable");
                foreach(DataRow row in returnSet.Tables[0].Rows)
                {
                    Int32.TryParse(row["ID"].ToString(), out int id);
                    Double.TryParse(row["QuantityInStock"].ToString(), out double quantity);
                    ingredientList.Add(new Ingredient(id, row["Name"].ToString()!, quantity, "Ounce"));
                }
                response.Object = ingredientList;
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
}
