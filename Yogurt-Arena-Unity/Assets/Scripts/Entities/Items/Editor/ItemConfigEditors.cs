using UnityEditor;
using UnityEngine.UIElements;

namespace Yogurt.Arena.Editor
{
    public abstract class ItemConfigEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            return new ItemConfigInspectorView(serializedObject, false);
        }
    }

    [CustomEditor(typeof(BoomerWeaponConfig)), CanEditMultipleObjects]
    public sealed class BoomerItemConfigEditor : ItemConfigEditor { }

    [CustomEditor(typeof(ChargerWeaponConfig)), CanEditMultipleObjects]
    public sealed class ChargerItemConfigEditor : ItemConfigEditor { }

    [CustomEditor(typeof(HealingPotionConfig)), CanEditMultipleObjects]
    public sealed class HealingItemConfigEditor : ItemConfigEditor { }

    [CustomEditor(typeof(RainConfig)), CanEditMultipleObjects]
    public sealed class RainItemConfigEditor : ItemConfigEditor { }

    [CustomEditor(typeof(RifleConfig)), CanEditMultipleObjects]
    public sealed class RifleItemConfigEditor : ItemConfigEditor { }
}
