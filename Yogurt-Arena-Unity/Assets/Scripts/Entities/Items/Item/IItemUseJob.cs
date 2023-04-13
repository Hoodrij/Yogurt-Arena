using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface IItemUseJob
    {
        public UniTask Run(ItemAspect item, AgentAspect owner);
    }
}