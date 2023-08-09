using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct SelfExplosionFactory : IItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            SelfExplosionConfig config = Query.Single<SelfExplosionConfig>();

            ItemAspect weapon = await new ItemFactoryJob().Run(config, owner); 
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}