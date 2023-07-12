using UnityEngine;

namespace Yogurt.Arena
{
    public interface IItemUseJob
    {
        public Awaitable Run(ItemAspect item);
    }
}