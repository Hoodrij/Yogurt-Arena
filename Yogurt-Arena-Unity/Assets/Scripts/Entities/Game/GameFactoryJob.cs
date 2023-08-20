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
                .Add(new Game())
                .Add(config)
                .Add(new Time());

            foreach (IEntityConfig iconfig in config.All)
            {
                EntityConfig entityConfig = new EntityConfig
                {
                    Components = iconfig.GetComponents()
                };
                
                Entity entity = Entity.Create()
                    .Add(entityConfig)
                    .SetParent(game);
                entityConfig.AppendTo(entity);
            }
        }
    }
}