namespace Yogurt.Arena
{
    public class EmptyGraphics : Graphic, IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
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