namespace Yogurt.Arena;

public class HealingPotionConfig : ScriptableObject, IComponent, IConfigSO
{
    public ItemConfig Item = new()
    {
        UseJob = new UseHealingPotionJob()
    };
    public int Amount;

    public IEnumerable<IComponent> GetComponents()
    {
        yield return this;
        yield return Item;
    }
}