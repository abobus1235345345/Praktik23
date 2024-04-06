using System;
using System.Collections.Generic;

public class EntityCache
{
    private static EntityCache instance;
    private Dictionary<string, List<object>> cache;

    private EntityCache()
    {
        cache = new Dictionary<string, List<object>>();
    }

    public static EntityCache Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new EntityCache();
            }
            return instance;
        }
    }

    public void AddEntity(string entityType, object entity)
    {
        if (!cache.ContainsKey(entityType))
        {
            cache[entityType] = new List<object>();
        }
        cache[entityType].Add(entity);
    }

    public bool ContainsEntity(string entityType, object entity)
    {
        return cache.ContainsKey(entityType) && cache[entityType].Contains(entity);
    }

    public bool ContainsEntityType(string entityType)
    {
        return cache.ContainsKey(entityType);
    }

    public void RegisterEntityType(string entityType)
    {
        if (!cache.ContainsKey(entityType))
        {
            cache[entityType] = new List<object>();
        }
    }

    public void RemoveEntity(string entityType, object entity)
    {
        if (cache.ContainsKey(entityType))
        {
            cache[entityType].Remove(entity);
        }
    }

    public void RemoveEntityType(string entityType)
    {
        if (cache.ContainsKey(entityType))
        {
            cache.Remove(entityType);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        EntityCache cache = EntityCache.Instance;

        cache.RegisterEntityType("Person");

        object person1 = new { Name = "Alice", Age = 30 };
        cache.AddEntity("Person", person1);

        Console.WriteLine("Person entity added to cache.");

        if (cache.ContainsEntityType("Person"))
        {
            Console.WriteLine("Person type is cached.");
        }

        if (cache.ContainsEntity("Person", person1))
        {
            Console.WriteLine("Person entity is cached.");
        }

        cache.RemoveEntity("Person", person1);
        if (!cache.ContainsEntity("Person", person1))
        {
            Console.WriteLine("Person entity removed from cache.");
        }

        cache.RemoveEntityType("Person");
        if (!cache.ContainsEntity("Person", person1))
        {
            Console.WriteLine("Person type removed from cache.");
        }
    }
}
