using UnityEngine;

namespace Yogurt.Arena
{
    public interface IQuest
    {
        Awaitable Run();
    }
}