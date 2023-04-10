using System;
using Random = UnityEngine.Random;

namespace Yogurt.Arena
{
    [Serializable]
    public struct MinMax
    {
        public int Min;
        public int Max;

        public int GetRandom()
        {
            return Random.Range(Min, Max);
        }
    }
}