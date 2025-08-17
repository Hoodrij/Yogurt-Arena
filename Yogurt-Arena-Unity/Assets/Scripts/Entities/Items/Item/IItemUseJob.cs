namespace Yogurt.Arena;

public interface IItemUseJob
{
    UniTask Run(ItemAspect item);
}