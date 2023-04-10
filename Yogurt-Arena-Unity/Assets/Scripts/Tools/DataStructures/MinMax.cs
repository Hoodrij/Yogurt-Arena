﻿using System;
using Random = UnityEngine.Random;

namespace Yogurt.Arena
{
    [Serializable]
    public struct MinMax
    {
        public float Min;
        public float Max;

        public float GetRandom()
        {
            return Random.Range(Min, Max);
        }
    }
}