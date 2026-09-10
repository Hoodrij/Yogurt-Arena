using UnityEngine.UIElements;

namespace Yogurt.Arena
{
    [RequireComponent(typeof(UIDocument))]
    [DefaultExecutionOrder(100)]
    public class UIElementsView : UIView
    {
        private void OnEnable()
        {
            VisualElement root = GetComponent<UIDocument>().rootVisualElement;
            root.pickingMode = PickingMode.Ignore;
            ((UIElementsHealthWidget)HealthWidget).Bind(root);
            ((UIElementsWeaponLifetimeWidget)WeaponLifetimeWidget).Bind(root);
            ((UIElementsGameOverWidget)GameOverWidget).Bind(root);
        }

        private void OnDisable()
        {
            ((UIElementsHealthWidget)HealthWidget).Unbind();
            ((UIElementsWeaponLifetimeWidget)WeaponLifetimeWidget).Unbind();
            ((UIElementsGameOverWidget)GameOverWidget).Unbind();
        }
    }
}
