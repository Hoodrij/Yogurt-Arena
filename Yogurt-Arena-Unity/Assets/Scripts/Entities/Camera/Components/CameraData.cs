using System;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class CameraData : IComponent
    {
        public Asset<CameraView> Asset;
        public float SmoothValue;
    }
}