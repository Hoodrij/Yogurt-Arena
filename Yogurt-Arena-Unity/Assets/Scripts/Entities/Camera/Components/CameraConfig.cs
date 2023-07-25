using System;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class CameraConfig : IComponent
    {
        public Asset<CameraView> Asset;
        public float SmoothValue;
    }
}