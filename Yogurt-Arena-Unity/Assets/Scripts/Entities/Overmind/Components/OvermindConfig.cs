using System;

namespace Yogurt.Arena
{
    [Serializable]
    public class OvermindConfig : IComponent
    {
        public MinMaxInt WaveAgentsCount;
        public int MinimumAgents;
        public MinMax WavesDelay;
    }
}