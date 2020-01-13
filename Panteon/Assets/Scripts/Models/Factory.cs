using Assets.Scripts.Interfaces;

public static class Factory
{
    public static IFactory GetFactoryOfType<T>() where T : new()
    {
        return (IFactory)new T();
    }
}
