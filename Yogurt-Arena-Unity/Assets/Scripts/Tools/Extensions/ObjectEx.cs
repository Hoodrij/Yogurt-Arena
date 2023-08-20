namespace Yogurt.Arena
{
    public static class ObjectEx
    {
        public static T log<T>(this T t, string prefix = "")
        {
            UnityEngine.Debug.Log(prefix + t);
            return t;
        }
    }
}