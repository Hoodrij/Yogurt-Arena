namespace Yogurt.Arena;

public static class IEnumerableEx
{
    public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
    {
        return !enumerable.Any();
    }

    public static T GetRandom<T>(this IEnumerable<T> enumerable)
    {
        if (enumerable.IsEmpty()) 
            return default;
            
        int index = enumerable.Count().RandomTo();
        return enumerable.ElementAt(index);
    }

    public static IEnumerable<T> Except<T>(this IEnumerable<T> enumerable, T item)
    {
        return enumerable.Where(t => !t.Equals(item));
    }
}