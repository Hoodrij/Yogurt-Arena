using UnityEngine;
using Random = System.Random;

namespace Yogurt.Arena
{
    public static class BoundsEx
    {
        public static Vector3 GetRandomPoint(this Bounds bounds, string seed = "random")
        {
            if (seed == "random")
                seed = UnityEngine.Time.realtimeSinceStartup.ToString();

            Random pseudoRandom = new Random(seed.GetHashCode());

            float x = pseudoRandom.Next((int) (bounds.min.x * 100), (int) (bounds.max.x * 100)) / 100f;
            float y = pseudoRandom.Next((int) (bounds.min.y * 100), (int) (bounds.max.y * 100)) / 100f;
            float z = pseudoRandom.Next((int) (bounds.min.z * 100), (int) (bounds.max.z * 100)) / 100f;
            return new Vector3(x, y, z);
        }
    }
}