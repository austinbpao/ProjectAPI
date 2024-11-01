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


namespace GroceryControllers.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public string GetTest(){
        return "Test";
    }

}