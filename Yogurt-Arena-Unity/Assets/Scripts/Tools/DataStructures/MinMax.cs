namespace Yogurt.Arena
{
    [Serializable]
    public struct MinMax
    {
        public float Min;
        public float Max;

        public MinMax(float min, float max)
        {
            Min = min;
            Max = max;
        }

        public float GetRandom()
        {
            return Random.Range(Min, Max);
        }
    }
}