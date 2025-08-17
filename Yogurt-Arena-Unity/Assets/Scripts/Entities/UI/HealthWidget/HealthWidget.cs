namespace Yogurt.Arena
{
    public class HealthWidget : MonoBehaviour, IComponent
    {
        [SerializeField] private Image image;

        private float currentPercentage;
        private Color colorRed = new Color(1f, 0.37f, 0.23f);
        private Color colorGreen = new Color(0f, 1f, 0.38f);
        private Color colorWhite = Color.white;
            
        public async UniTaskVoid SetHealth(float percentage)
        {
            Color animationColor = GetColor();
            currentPercentage = percentage;
            
            float duration = 0.1f;
            image.DOKill();
            image.rectTransform.DOKill();
            
            image.DOFillAmount(percentage, duration);
            image.rectTransform.DOShakeScale(duration, new Vector3(0, 1f, 0));
            image.DOColor(animationColor, duration);
            await Wait.Seconds(duration);
            if (image == null)
            {
                return;
            }
            image.rectTransform.DOScale(1, duration);
            image.DOColor(Color.white, duration);
            return;

            Color GetColor()
            {
                if (currentPercentage == 0)
                    return colorWhite;
                if (currentPercentage < percentage)
                    return colorGreen;
                else
                    return colorRed;
            }
        }
    }
}