using Cysharp.Threading.Tasks;

namespace Yogurt.Arena.Quest
{
    public interface IQuest
    {
        UniTask Run();
    }
}