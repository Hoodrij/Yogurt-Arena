using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class UseRifleJob : IItemUseJob
    {
        public async UniTask Run(ItemAspect itemAspect, Entity owner)
        {
            IAsset<BulletView> bulletAsset = Query.Single<Assets>().RifleBullet;

            while (itemAspect.Exist())
            {
                await WaitForTarget(owner);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(bulletAsset, owner);
                new RifleBulletBehaviorJob().Run(bullet); 

                await UniTask.Delay(0.1f.ToSeconds());
            }
        }

        private async UniTask WaitForTarget(Entity owner)
        {
            AgentBattleState battleState = owner.Get<AgentBattleState>();
            await UniTask.WaitWhile(() => !battleState.Target.Exist);
        }
    }
}