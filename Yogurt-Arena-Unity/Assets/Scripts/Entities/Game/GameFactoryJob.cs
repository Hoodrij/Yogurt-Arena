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
                .Add(new Time());

            LoadConfig();
            return;

            void LoadConfig()
            { 
                Config config = Resources.Load<Config>("Config");
                foreach (IConfigSO configSO in config.All)
                {
                    ConfigEntity configEntity = new ConfigEntity
                    {
                        Components = configSO.GetComponents()
                    };

                    Entity.Create()
                        .Add(configEntity)
                        .Add(configEntity.Components)
                        .SetParent(game);
                }
            }
        }
    }
}