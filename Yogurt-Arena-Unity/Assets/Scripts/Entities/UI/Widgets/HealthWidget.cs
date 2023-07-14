using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Yogurt.Arena
{
    public class HealthWidget : MonoBehaviour
    {
        [SerializeField] Image image;
            
        public async void SetHealth(float percentage)
        {
            float duration = 0.1f;
            Color color = new Color(1f, 0.25f, 0.28f);
            image.DOKill();
            image.rectTransform.DOKill();
            
            image.DOFillAmount(percentage, duration);
            image.rectTransform.DOShakeScale(duration, new Vector3(0, 1f, 0));
            image.DOColor(color, 0.1f);
            await Wait.Seconds(duration);
            image.rectTransform.DOScale(1, duration);
            image.DOColor(Color.white, duration);
        }
    }
}