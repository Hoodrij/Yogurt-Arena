namespace Yogurt.Arena
{
    public class HealingPotionConfig : ScriptableObject, IComponent, IConfigSO, IBlueprint
    {
        public ItemConfig Item = new()
        {
            UseJob = new UseHealingPotionJob()
        };
        public int Amount;
        
        public void Populate(Entity entity)
        {
            entity.Add(this);
        }
    }
}