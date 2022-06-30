namespace sandwichshop.Menu;

public class Singleton<T> where T : new()
{
    private static T instance;
    private static object _lock = new object();
    public static T Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance = new T();
                }
            }

            return instance;
        }
    }
}