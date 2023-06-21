using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public class ItemSpotView : MonoBehaviour, IComponent
    {
        [Serializable]
        private class EItemTypeToViewDict : SerializableDictionary<EItemType, Transform> { }

        [SerializeField] private EItemTypeToViewDict map;
        
        
        private async void Awake()
        {
            transform.DOScale(0, 0);
            
            ItemSpotAspect itemSpot = await new ItemSpotFactoryJob().Run(this);
            new ItemSpotBehaviorJob().Run(itemSpot);
        }

        public async UniTask Show(EItemType type)
        {
            foreach (var pair in map)
            {
                bool isActive = pair.Key == type;
                pair.Value.gameObject.SetActive(isActive);
            }

            transform.DOScale(1, 0.2f).SetEase(Ease.OutBack, 6);
        }
        
        public async UniTask Hide()
        {
            transform.DOScale(0, 0.2f).SetEase(Ease.InBack, 6);
        }
    }
}