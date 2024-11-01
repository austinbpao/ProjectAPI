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
public class DrinkController : ControllerBase
{
    private readonly ILogger<DrinkController> _logger;

    public DrinkController(ILogger<DrinkController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public OrderResponse<List<Recipe>> GetDrinkList(){
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try
        {
            var appsettings = System.Configuration.ConfigurationManager.AppSettings;
            List<Recipe> recipeList = new List<Recipe>();
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"])){
                string sql = @"EXEC GetDrinkList";
                string sql2 = @"EXEC GetDrinkListIngredients";
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
    public OrderResponse<List<Recipe>> GetDrinkListByIds([FromBody] IEnumerable<int> ids){
        OrderResponse<List<Recipe>> response = new OrderResponse<List<Recipe>>();
        try 
        {
            var appsettings = System.Configuration.ConfigurationManager.AppSettings;
            List<Recipe> recipeList = new List<Recipe>();
            DataTable idDataTable = new DataTable();
            idDataTable.Columns.Add();
            foreach(int id in ids)
            {
                idDataTable.Rows.Add(id);
            }
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                conn.Open();
                DataSet returnSet = SqlUtils.SqlUtils.GetByIdTable(conn, idDataTable, @"GetDrinkListByIDs");
                foreach(DataRow row in returnSet.Tables[0].Rows)
                {
                        Int32.TryParse(row[2].ToString(), out int servings);
                        Int32.TryParse(row[0].ToString(), out int id);
                        recipeList.Add(new Recipe(id, row[1].ToString()!, servings));
                }
            response.statusCode = "200";
            response.message = "Okay";
            response.Object=recipeList;
            }
        }
    catch (Exception ex)
    {
        throw new HttpRequestException(ex.Message);
    }
    return response;
    }
}