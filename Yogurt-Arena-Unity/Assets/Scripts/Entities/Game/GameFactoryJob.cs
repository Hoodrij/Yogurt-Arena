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
                .Add(new Time())
                .Add(config)
                ;

            foreach (IEntityConfig iConfig in config.All)
            {
                EntityConfig entityConfig = new EntityConfig
                {
                    Components = iConfig.GetComponents()
                };
                
                Entity entity = Entity.Create()
                    .Add(entityConfig)
                    .Add(entityConfig.Components)
                    .SetParent(game);
            }
        }
    }
}