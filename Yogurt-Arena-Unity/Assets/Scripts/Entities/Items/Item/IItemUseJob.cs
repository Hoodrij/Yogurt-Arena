using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface IItemUseJob
    {
        UniTask Run(ItemAspect item);
    }
}