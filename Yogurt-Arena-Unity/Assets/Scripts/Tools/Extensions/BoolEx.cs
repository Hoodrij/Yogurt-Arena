using UnityEngine;

namespace Yogurt.Arena
{
    public static class BoolEx
    {
        public static bool RandomBool(this bool b)
        {
            return Random.value > 0.5f;
        }
    }
}