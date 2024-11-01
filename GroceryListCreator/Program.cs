using GroceryClassLib;
using Microsoft.Data.SqlClient;
using System.Security;
using GroceryControllers.Controllers;
using System.Net.Http;
using System.Net;

public class Program
{
    static void Main(string[] args)
    {
        var appsettings = System.Configuration.ConfigurationManager.AppSettings;
        using(SqlConnection conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["GroceryConnection"])){
            string sql = @"EXEC GetRecipeById";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.Add(new SqlParameter());
            SecureString pwd = new NetworkCredential("",appsettings["SqlPassword"]).SecurePassword;
            pwd.MakeReadOnly();
            conn.Credential = new SqlCredential(appsettings["SqlUser"], pwd);
            conn.Open();
            using(SqlDataReader reader = command.ExecuteReader()){
                while (reader.Read()){
                    Console.WriteLine($"{reader[0]}, {reader[1]}");
                }
            }
        }
    }
}
