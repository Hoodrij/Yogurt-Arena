namespace Yogurt.Arena
{
    public class InputConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public Asset<InputFieldView> Asset;
        public float AccumulativeValue;
        public float DeAccumulativeValue;
        public float Sensitivity;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}