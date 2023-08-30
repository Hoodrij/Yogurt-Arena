using DG.Tweening;
using UnityEngine;

namespace Yogurt.Arena
{
    public class GameOverWidget : MonoBehaviour
    {
        [SerializeField] private CanvasGroup canvasGroup;
        
        public Event OnRestartClick = new Event();

        public void Show()
        {
            gameObject.SetActive(true);
            canvasGroup.DOFade(1, 0.3f);
        }

        public void RestartClick() => OnRestartClick.Fire();
    }
}