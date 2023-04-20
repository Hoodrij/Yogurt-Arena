namespace Yogurt.Arena
{
    public class Item : IComponent
    {
        public IItemUseJob Job;
        public AgentAspect Owner;
    }
}