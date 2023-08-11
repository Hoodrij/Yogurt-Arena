using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Yogurt.Arena
{
    public class WeaponLifetimeWidget : MonoBehaviour
    {
        [SerializeField] private Image image;
        
        public void SetProgress(float value)
        {
            image.DOFillAmount(value, 0.1f);
        }
    }
}