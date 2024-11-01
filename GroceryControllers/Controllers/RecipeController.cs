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

public class RecipeController : ControllerBase
{
    private readonly ILogger<IngredientController> _logger;

    public RecipeController(ILogger<IngredientController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public OrderResponse<List<Recipe>> GetRecipe()
    {
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {

            response.statusCode = "200";
            response.message = "Okay";
        }
        catch (Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }
    [HttpGet]
    public OrderResponse<List<Recipe>> GetDrinkList()
    {
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {
            var appsettings = System.Configuration.ConfigurationManager.AppSettings;
            List<Recipe> recipeList = new List<Recipe>();
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"])){
                string sql = @"EXEC GetRecipeList";
                string sql2 = @"EXEC GetRecipeListIngredients";
                List<string> commands = new List<string>() {sql, sql2};
                conn.Open();
                DataSet resultSet = SqlUtils.SqlUtils.GetMultipleTables(conn, commands);
                foreach(DataRow row in resultSet.Tables[0].Rows)
                {
                        recipeList.Add(new Recipe((int)row["Id"], row["Name"].ToString() ?? "", (int)row["Servings"], row["Category"].ToString() ?? "", row["Subcategory"].ToString() ?? ""));
                }
                foreach(Recipe recipe in recipeList){
                    recipe.Ingredients.AddRange(resultSet.Tables[1].AsEnumerable().Where(i => (int)i["RecipeId"] == recipe.Id)
                    .Select(i => new Ingredient((int)i["IngredientId"], i["IngredientName"].ToString() ?? "", Decimal.ToDouble((decimal)i["Quantity"]), i["UnitName"].ToString() ?? "")));
                }
            }
            response.statusCode = "200";
            response.message = "Okay";
            response.Object=recipeList;
        }
        catch (Exception ex){
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }

    [HttpPost]
    public OrderResponse<List<Recipe>> PostRecipe()
    {
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {

            response.statusCode = "200";
            response.message = "Okay";
        }
        catch (Exception ex)
        {
            throw new HttpRequestException(ex.Message);
        }
        return response;
    }

    [HttpPut]
    public OrderResponse<List<Recipe>> PutRecipe()
    {
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {

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
    public OrderResponse<List<Recipe>> UpdateRecipe([FromBody]Recipe input)
    {
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                List<string> dtColumns = new List<string>() {"Id"};
                List<object> queryValues = new List<object>() {input};
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<Recipe>(conn, dtColumns, queryValues, "UpdateRecipes", "@RecipeTable");
                response.Object = new List<Recipe>();
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
    public OrderResponse<List<Recipe>> DeleteRecipe()
    {
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {

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