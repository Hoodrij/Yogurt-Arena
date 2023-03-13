using UnityEngine;

namespace Yogurt.Arena
{
    public static class FloatEx
    {
        public static float RandomTo(this float f)
        {
            return Random.Range(0, f);
        }
    }
}