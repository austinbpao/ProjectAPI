using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Configuration;
using System.Security;
using System.Net.Http;
using System.Net;
using System.Linq;
using System.Data;
using System.Reflection;


namespace SqlUtils;

public class SqlUtils
{
    public static DataSet GetDataSet(SqlConnection conn, string command)
    {
        DataSet returnSet = new DataSet();
        SqlCommand sqlCommand = new SqlCommand();
        using(conn)
        {
        sqlCommand.Connection = conn;
        sqlCommand.CommandText = command;
        using(SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.SelectCommand = sqlCommand;
                adapter.Fill(returnSet);
            }
        }
        return returnSet;
    }

    public static DataSet GetMultipleTables(SqlConnection conn, List<string> commands)
    {
        DataSet returnSet = new DataSet();
        SqlCommand sqlCommand = new SqlCommand();
        using(conn)
        {
            sqlCommand.Connection = conn;
            foreach(string command in commands)
            {
                sqlCommand.CommandText = command;
                using(SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    adapter.SelectCommand = sqlCommand;
                    adapter.Fill(returnSet.Tables.Add());
                }
            }
        }
        return returnSet;
    }

    public static DataSet GetByIdTable(SqlConnection conn, DataTable idDataTable, string procName)
    {
        DataSet returnSet = new DataSet();
        SqlCommand sqlCommand = new SqlCommand(procName, conn);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        SqlParameter idTable = sqlCommand.Parameters.AddWithValue("@IdTable", idDataTable);
        idTable.SqlDbType = SqlDbType.Structured;
        using(SqlDataAdapter adapter = new SqlDataAdapter())
        {
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(returnSet);
        }
        
        return returnSet;
    }

    public static DataSet SubmitTypedTable<T>(SqlConnection conn, List<string> columns, List<object> values, string procName, string parameterName)
    {
        DataSet returnSet = new DataSet();
        SqlCommand sqlCommand = new SqlCommand(procName, conn);
        sqlCommand.CommandType = CommandType.StoredProcedure;
        DataTable dt = new DataTable();
        foreach(string column in columns)
        {
            dt.Columns.Add(column);
        }
        dt.Rows.Add();
        for(int i=0; i < values.Count; i++)
        {
            dt.Rows[0][i] = values[i];
        }
        SqlParameter idTable = sqlCommand.Parameters.AddWithValue(parameterName, dt);
        idTable.SqlDbType = SqlDbType.Structured;
        using(SqlDataAdapter adapter = new SqlDataAdapter())
        {
            adapter.SelectCommand = sqlCommand;
            adapter.Fill(returnSet);
        }
        
        return returnSet;
    }

    private static DataTable DataTableFromObject(object obj)
    {
        DataTable dt = new DataTable();
        dt.Rows.Add();
        foreach(PropertyInfo propertyInformation in obj.GetType().GetProperties())
        {
            dt.Columns.Add(propertyInformation.Name);
            dt.Rows[0][propertyInformation.Name] = obj.GetType().GetProperty(propertyInformation.Name)!.GetValue(obj);
            Console.WriteLine(obj.GetType().GetProperty(propertyInformation.Name)!.GetValue(obj)!.ToString());
        }
        return dt;
    }

}
