namespace Yogurt.Arena
{
    public class WeaponLifetimeWidget : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public virtual void SetProgress(float value)
        {
            image.DOKill();
            image.DOFillAmount(value, 0.1f);
        }
    }
}
