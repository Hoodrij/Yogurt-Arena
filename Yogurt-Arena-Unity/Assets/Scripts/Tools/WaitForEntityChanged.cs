using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct WaitForEntityChanged
    {
        public async Awaitable Run(Func<Entity> entityGetter)
        {
            Entity initialEntity = entityGetter.Invoke();

            await Wait.While(() => initialEntity == entityGetter.Invoke());
        }
    }
}