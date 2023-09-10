using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct GameFactoryJob
    {
        public async UniTask Run()
        {
            Entity game = Entity.Create()
                .Add(new Game())
                .Add(new Time())
                ;

            Config config = Resources.Load<Config>("Config");
            foreach (IEntityConfig iConfig in config.All)
            {
                ConfigOfEntity configOfEntity = new ConfigOfEntity
                {
                    Components = iConfig.GetComponents()
                };

                Entity.Create()
                    .Add(configOfEntity)
                    .Add(configOfEntity.Components)
                    .SetParent(game);
            }
        }
    }
}