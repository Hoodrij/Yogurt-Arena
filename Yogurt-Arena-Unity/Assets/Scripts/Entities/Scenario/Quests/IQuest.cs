using UnityEngine;

namespace Yogurt.Arena.Quest
{
    public interface IQuest
    {
        Awaitable Run();
    }
}