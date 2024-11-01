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

public class GenericController : ControllerBase
{
    private readonly ILogger<IngredientController> _logger;

    public GenericController(ILogger<IngredientController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public OrderResponse<List<Object>> GetGeneric()
    {
        OrderResponse<List<Object>> response = new OrderResponse<List<Object>>();
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

    [HttpPost]
    public OrderResponse<List<Object>> PostGeneric()
    {
        OrderResponse<List<Object>> response = new OrderResponse<List<Object>>();
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
    public OrderResponse<List<Object>> PutGeneric()
    {
        OrderResponse<List<Object>> response = new OrderResponse<List<Object>>();
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
    public OrderResponse<List<Object>> UpdateGeneric([FromBody]Object input)
    {
        OrderResponse<List<Object>> response = new OrderResponse<List<Object>>();
        try
        {
            using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"]))
            {
                List<string> dtColumns = new List<string>() {"Id"};
                List<object> queryValues = new List<object>() {input};
                DataSet returnSet = SqlUtils.SqlUtils.SubmitTypedTable<Object>(conn, dtColumns, queryValues, "UpdateObjects", "@ObjectTable");
                response.Object = new List<object>();
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
    public OrderResponse<List<Object>> DeleteGeneric()
    {
        OrderResponse<List<Object>> response = new OrderResponse<List<Object>>();
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