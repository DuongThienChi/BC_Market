using System.Collections.Generic;

/// <summary>
/// Manages session data for the application.
/// </summary>
public static class SessionManager
{
    private static Dictionary<string, object> sessionData = new Dictionary<string, object>();

    /// <summary>
    /// Sets a value in the session data.
    /// </summary>
    /// <param name="key">The key for the session data.</param>
    /// <param name="value">The value to be stored.</param>
    public static void Set(string key, object value)
    {
        sessionData[key] = value;
    }

    /// <summary>
    /// Gets a value from the session data.
    /// </summary>
    /// <param name="key">The key for the session data.</param>
    /// <returns>The value associated with the specified key, or null if the key does not exist.</returns>
    public static object Get(string key)
    {
        sessionData.TryGetValue(key, out var value);
        return value;
    }

    /// <summary>
    /// Removes a value from the session data.
    /// </summary>
    /// <param name="key">The key for the session data to be removed.</param>
    public static void Remove(string key)
    {
        sessionData.Remove(key);
    }
}
