using Cysharp.Threading.Tasks;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class UseRifleJob : ItemUseJob
    {
        public async UniTask Run(Entity owner)
        {
            Asset<BulletView> asset = Query.Single<Assets>().RifleBullet;

            while (owner.Exist)
            {
                await WaitForTarget(owner);
                
                BulletAspect bullet = await new BulletFactoryJob().Run(asset, owner);
                new FireBulletJob().Run(bullet);

                await UniTask.Delay(0.1f.Seconds());
            }
        }

        private async UniTask WaitForTarget(Entity owner)
        {
            AgentBattleState battleState = owner.Get<AgentBattleState>();
            await UniTask.WaitWhile(() => !battleState.Target.Exist);
        }
    }
}