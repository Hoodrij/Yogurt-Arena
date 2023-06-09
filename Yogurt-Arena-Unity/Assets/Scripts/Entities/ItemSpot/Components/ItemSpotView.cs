using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotView : MonoBehaviour, IComponent
    {
        private async void Awake()
        {
            transform.DOScale(0, 0);
            
            ItemSpotAspect itemSpot = await new ItemSpotFactoryJob().Run(this);
            new ItemSpotBehaviorJob().Run(itemSpot);
        }

        public async UniTask Show()
        {
            transform.DOScale(1, 0.2f).SetEase(Ease.OutBack, 4);
        }
        
        public async UniTask Hide()
        {
            transform.DOScale(0, 0.2f).SetEase(Ease.InBack, 4);
        }
    }
}