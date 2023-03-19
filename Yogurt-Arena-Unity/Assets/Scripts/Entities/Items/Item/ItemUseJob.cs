using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface ItemUseJob
    {
        public UniTask Run(Entity owner);
    }
}