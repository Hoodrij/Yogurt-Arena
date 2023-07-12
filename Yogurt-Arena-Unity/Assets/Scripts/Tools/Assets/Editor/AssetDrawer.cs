using UnityEditor;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Roguelike.Tools
{
    [CustomPropertyDrawer(typeof(Asset<>))]
    public class AssetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Draw(position, property, label);
        }

        public static void Draw(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty prefabProp = property.FindPropertyRelative(nameof(Asset<Component>.Prefab));

            EditorGUI.BeginProperty(position, label, property);
       
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);
            EditorGUI.PropertyField(position, prefabProp, GUIContent.none);
 
            EditorGUI.EndProperty();
        }
    }
    
    [CustomPropertyDrawer(typeof(PooledAsset<>))]
    public class PooledAssetDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            SerializedProperty assetProp = property.FindPropertyRelative(nameof(PooledAsset<Component>.asset));
            AssetDrawer.Draw(position, assetProp, label);
        }
    }
}