using UnityEngine;
using UnityEngine.UI;

namespace Yogurt.Arena
{
    public class PlayerHealthWidget : MonoBehaviour
    {
        [SerializeField] Image image;
            
        public void SetHealth(float percentage)
        {
            image.fillAmount = percentage;
        }
    }
}