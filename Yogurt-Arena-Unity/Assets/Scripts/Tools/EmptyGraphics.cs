using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Yogurt.Arena.Tools
{
    public class EmptyGraphics : Graphic, IPointerClickHandler,
        IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public void OnPointerClick(PointerEventData eventData)
        {
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
        }
        public void OnDrag(PointerEventData eventData)
        {
        }
        public void OnEndDrag(PointerEventData eventData)
        {
        }
        
        public override void SetMaterialDirty() { }
        public override void SetVerticesDirty() { }
    }
}