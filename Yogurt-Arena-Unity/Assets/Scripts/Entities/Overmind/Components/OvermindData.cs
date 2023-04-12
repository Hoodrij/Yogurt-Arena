using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class OvermindData : IComponent
    {
        public MinMaxInt WaveAgentsCount;
        public int MinimumAgents;
        public MinMax WavesDelay;
    }
}