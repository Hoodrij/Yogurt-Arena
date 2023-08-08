using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Config config = Resources.Load<Config>("Config");

            Entity game = Entity.Create()
                .Add<Game>()
                .Add(config)
                .Add<Time>();

            foreach (IConfig iconfig in config.All)
            {
                Entity entity = Entity.Create()
                    .SetParent(game);
                iconfig.AppendTo(entity);
            }
        }
    }
}