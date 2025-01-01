using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.IO;

/// <summary>
/// Helper class for retrieving configuration settings.
/// </summary>
public static class ConfigurationHelper
{
    /// <summary>
    /// Gets the connection string from the appsettings.json file.
    /// </summary>
    /// <param name="name">The name of the connection string to retrieve.</param>
    /// <returns>The connection string.</returns>
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
