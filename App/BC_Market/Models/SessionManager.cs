public static class SessionManager
{
    private static Dictionary<string, object> sessionData = new Dictionary<string, object>();

    public static void Set(string key, object value)
    {
        sessionData[key] = value;
    }

    public static object Get(string key)
    {
        sessionData.TryGetValue(key, out var value);
        return value;
    }

    public static void Remove(string key)
    {
        sessionData.Remove(key);
    }
}
