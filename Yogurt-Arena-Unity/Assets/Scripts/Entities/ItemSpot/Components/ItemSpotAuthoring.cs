using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotAuthoring : MonoBehaviour
    {
        public EItemType ItemType;
        public float Radius;
        public LayerMask Mask;
        
        private async void Awake()
        {
            ItemSpotAspect itemSpot = await new ItemSpotFactoryJob().Run(this);
            new ItemSpotBehaviorJob().Run(itemSpot);
        }
    }
}