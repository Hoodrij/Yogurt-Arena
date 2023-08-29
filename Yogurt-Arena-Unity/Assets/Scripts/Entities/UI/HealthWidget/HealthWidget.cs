using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Yogurt.Arena
{
    public class HealthWidget : MonoBehaviour, IComponent
    {
        [SerializeField] Image image;

        private float currentPercentage;
        private Color colorRed = new Color(1f, 0.25f, 0.28f);
        private Color colorGreen = new Color(0.24f, 1f, 0.13f);
            
        public async UniTaskVoid SetHealth(float percentage)
        {
            Color animationColor = currentPercentage < percentage
                ? colorGreen
                : colorRed;
            
            currentPercentage = percentage;
            
            float duration = 0.1f;
            image.DOKill();
            image.rectTransform.DOKill();
            
            image.DOFillAmount(percentage, duration);
            image.rectTransform.DOShakeScale(duration, new Vector3(0, 1f, 0));
            image.DOColor(animationColor, 0.1f);
            await Wait.Seconds(duration);
            image.rectTransform.DOScale(1, duration);
            image.DOColor(Color.white, duration);
        }
    }
}