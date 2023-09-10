using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct SpawnWorldHealthWidget
    {
        public async UniTask Run(AgentAspect owner)
        {
            WorldUIView worldUI = Query.Single<WorldUIView>();
            UIConfig config = Query.Single<UIConfig>();

            HealthWidget healthWidget = await config.WorldHealthWidget.Spawn();
            healthWidget.transform.SetParent(worldUI.transform, false);
            
            Entity entity = World.Create()
                .AddLink(healthWidget.gameObject)
                .Add(healthWidget);
            
            entity.SetParent(owner.Entity);
            owner.Health.HealthWidget = healthWidget;
            healthWidget.SetHealth(1);
            
            entity.Run(UpdatePosition);
            return;
            
            
            void UpdatePosition()
            {
                Vector3 position = owner.Body.Position;
                healthWidget.transform.position = position;
            }
        }
    }
}