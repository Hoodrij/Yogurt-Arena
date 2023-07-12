using System;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class InputData : IComponent
    {
        public Asset<InputFieldView> Asset;
        public float AccumulativeValue;
        public float DeAccumulativeValue;
        public float Sensitivity;
    }
}