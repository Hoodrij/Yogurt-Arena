namespace Yogurt.Arena;

public class MoveInputReader : MonoBehaviour, IComponent, IPointerClickHandler
{
    public bool HasClick { get; set; }
    public Vector2 ClickScreenPosition { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        HasClick = true;
        ClickScreenPosition = eventData.position;
    }
}