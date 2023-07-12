using System;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class BeaconData : IComponent
    {
        public Asset<BeaconView> Asset;
        public float SmoothValue;
        public float Elasticity;
    }
}