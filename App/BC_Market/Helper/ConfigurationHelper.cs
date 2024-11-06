using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

public static class ConfigurationHelper // Get connection string from appsettings.json
{
    public static string GetConnectionString(string name) 
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
        string connectionString = configuration.GetConnectionString(name);
        return connectionString;
    }
}
