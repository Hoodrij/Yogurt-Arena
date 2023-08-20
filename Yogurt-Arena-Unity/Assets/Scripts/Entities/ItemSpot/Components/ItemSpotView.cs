using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotView : MonoBehaviour, IComponent
    {
        [Serializable]
        private class EItemTypeToViewDict : SerializableDictionary<ItemType, Transform> { }

        [SerializeField] private EItemTypeToViewDict map;
        
        
        private async void Awake()
        {
            transform.DOScale(0, 0);
            
            ItemSpotAspect itemSpot = await new ItemSpotFactoryJob().Run(this);
            new ItemSpotBehaviorJob().Run(itemSpot);
        }

        public async UniTask Show(ItemType type)
        {
            if (!map.TryGetValue(type, out Transform icon))
            {
                icon = map[ItemType.Any];
            }

            icon.gameObject.SetActive(true);
            transform.DOScale(1, 0.2f).SetEase(Ease.OutBack, 6);
        }
        
        public async UniTask Hide()
        {
            transform.DOScale(0, 0.2f).SetEase(Ease.InBack, 6);
        }
    }
}