namespace Yogurt.Arena
{
    public class InputConfig : ScriptableObject, IComponent, IConfigSO
    {
        public Asset<InputFieldView> Asset;
        public float AccumulativeValue;
        public float DeAccumulativeValue;
        public float Sensitivity;
    }
}