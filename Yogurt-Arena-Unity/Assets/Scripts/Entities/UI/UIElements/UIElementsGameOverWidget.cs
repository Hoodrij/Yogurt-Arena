using UnityEngine.UIElements;
using ToolkitButton = UnityEngine.UIElements.Button;

namespace Yogurt.Arena
{
    public class UIElementsGameOverWidget : GameOverWidget
    {
        private VisualElement overlay;
        private ToolkitButton restartButton;

        public void Bind(VisualElement root)
        { 
            Unbind();
            overlay = root.Q<VisualElement>("game-over");
            overlay.RemoveFromClassList("game-over--visible");
            overlay.pickingMode = PickingMode.Ignore;
            restartButton = root.Q<ToolkitButton>("restart-button");
            restartButton.clicked += RestartClick;
        }

        public void Unbind()
        {
            if (restartButton != null)
                restartButton.clicked -= RestartClick;
            restartButton = null;
            overlay = null;
        }

        public override void Show()
        {
            if (overlay == null)
                Bind(GetComponent<UIDocument>().rootVisualElement);

            overlay.pickingMode = PickingMode.Position;
            overlay.AddToClassList("game-over--visible");
            restartButton.Focus();
        }
    }
}
