using UnityEngine;
using UnityEngine.EventSystems;

namespace Yogurt.Arena
{
    public class MoveInputReader : MonoBehaviour, IComponent, IPointerDownHandler, IPointerUpHandler
    {
        public Vector3? ScreenPosition;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            ScreenPosition = eventData.position.ToV3XY();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            ScreenPosition = null;
        }
    }
}