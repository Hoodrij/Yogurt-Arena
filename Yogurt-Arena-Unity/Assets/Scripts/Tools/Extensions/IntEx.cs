namespace Yogurt.Arena
{
    public static class IntEx
    {
        public static TimeSpan ToSeconds(this int i)
        {
            return TimeSpan.FromSeconds(i);
        }
        
        public static int RandomTo(this int i)
        {
            return UnityEngine.Random.Range(0, i);
        }
        
        public static void Clamp(ref this int i, int min, int max)
        {
            i = Mathf.Clamp(i, min, max);
        }
    }
}