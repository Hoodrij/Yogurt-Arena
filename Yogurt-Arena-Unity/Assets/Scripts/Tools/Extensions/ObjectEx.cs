namespace Yogurt.Arena
{
    public static class ObjectEx
    {
        public static T log<T>(this T t)
        {
            UnityEngine.Debug.Log(t);
            return t;
        }
    }
}