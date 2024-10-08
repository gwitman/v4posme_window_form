﻿using System.Data;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using v4posme_library.Models;

namespace v4posme_library.Libraries.CustomModels;

class BdModel : IBdModel
{
    public void ExecuteSqlRaw(string? query)
    {
        var context = new DataContext();
        context.Database.ExecuteSqlRaw(query);
    }
    public void ExecuteProcedure(string? query)
    {
        using var connection = new MySqlConnection(VariablesGlobales.ConnectionString);
        connection.Open();
        var command = new MySqlCommand(query, connection);
        command.ExecuteNonQuery(); 
        connection.Close();
    }

    public T ExecuteRenderWidthParameter<T>(string? query, object[] parameter)
    {
        var context = new DataContext();
        return (T)context.Database.SqlQueryRaw<T>(query, parameter);
    }

    public T ExecuteRender<T>(string? query)
    {
        var context = new DataContext();
        return (T)context.Database.SqlQueryRaw<T>(query);
    }

    public List<Dictionary<string?, object>>? ExecuteRenderQueryable(string? query)
    {
        var conn = new MySqlConnection(VariablesGlobales.ConnectionString);
        var list = new List<Dictionary<string?, object>>();
        try
        {
            conn.Open();
            var cmd = new MySqlCommand(query, conn);
            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var dataRow = new Dictionary<string?, object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    dataRow.Add(reader.GetName(i), reader.GetValue(i));
                }

                list.Add(dataRow);
            }

            reader.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
            return null;
        }

        conn.Close();
        return list;
    }
}