namespace Yogurt.Arena
{
    public class ItemSpotView : MonoBehaviour, IComponent
    {
        [Serializable]
        private class ItemTypeToViewDict : SerializableDictionary<ItemType, Transform> { }

        [SerializeField] private ItemTypeToViewDict map;
        
        private void Awake()
        {
            Hide(0);

            new ItemSpotFactoryJob().Run(this).Forget();
        }

        public void Show(ItemType type)
        {
            foreach (Transform otherIcon in map.Values)
            {
                otherIcon.gameObject.SetActive(false);
            }
            
            if (!map.TryGetValue(type, out Transform icon))
            {
                icon = map[ItemType.Empty];
            }
            icon.gameObject.SetActive(true);
            transform.DOKill();
            transform.DOScale(1, 0.2f).SetEase(Ease.OutBack, 6);
        }
        
        public void Hide(float duration = 0.2f)
        {
            transform.DOKill();
            transform.DOScale(0, duration).SetEase(Ease.InBack, 6);
        }
    }
}