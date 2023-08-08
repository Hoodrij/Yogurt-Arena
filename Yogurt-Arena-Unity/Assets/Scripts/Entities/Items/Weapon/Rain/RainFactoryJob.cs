using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct RainFactoryJob : IItemFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            RainConfig config = Query.Single<RainConfig>();

            ItemAspect weapon = await new ItemFactoryJob().Run(config, owner); 
            weapon.Add(new WeaponClipState
            {
                CurrentAmmo = config.Clip.BulletsInClip
            });
            weapon.Add(owner.BattleState);
            
            new SetWeaponJob().Run(owner, weapon);
            new CommonTargetDetectionJob().Run(weapon);
            
            return weapon;
        }
    }
}