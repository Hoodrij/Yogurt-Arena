using UnityEngine;
using UnityEngine.EventSystems;

namespace Yogurt.Arena
{
    public class MoveInputReader : MonoBehaviour, IComponent, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        public Vector2 Delta { get; set; }
        public bool IsDown { get; private set; }

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
        }

        public void OnDrag(PointerEventData eventData)
        {
            Delta = eventData.delta / Screen.dpi;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            IsDown = true;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            IsDown = false;
        }
    }
}