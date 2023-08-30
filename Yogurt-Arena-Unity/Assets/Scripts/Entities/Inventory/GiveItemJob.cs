using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct GiveItemJob
    {
        public async UniTask Run(ItemType itemType, AgentAspect owner)
        {
            if (itemType == ItemType.Empty)
                return;
            
            ItemAspect item = await new ItemFactoryJob().Run(itemType, owner);
            UpdateWeapon();

            item.Config.UseJob.Run(item);
            return;


            void UpdateWeapon()
            {
                if (!item.Config.Tags.HasFlag(ItemTags.Weapon))
                    return;
                if (!owner.Exist())
                    return;

                owner.Inventory.Weapon.Kill();
                owner.Inventory.Weapon = item;
            }
        }
    }
}