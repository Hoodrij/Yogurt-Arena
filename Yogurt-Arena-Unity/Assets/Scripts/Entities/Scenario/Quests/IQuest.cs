using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public interface IQuest
    {
        UniTask Run();
    }
}