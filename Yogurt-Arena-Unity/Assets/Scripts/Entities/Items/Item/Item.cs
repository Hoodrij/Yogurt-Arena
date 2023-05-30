namespace Yogurt.Arena
{
    public class Item : IComponent
    {
        public IItemUseJob Job;
        public EItemType Type;
    }
}