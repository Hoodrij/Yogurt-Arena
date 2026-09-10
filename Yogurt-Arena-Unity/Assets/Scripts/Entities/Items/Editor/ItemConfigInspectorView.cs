using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Yogurt.Arena.Editor
{
    public sealed class ItemConfigInspectorView : VisualElement
    {
        internal const string StylePath = "Assets/Scripts/Entities/Items/Editor/ItemConfigEditor.uss";
        private readonly SerializedObject data;
        private readonly List<Section> sections = new List<Section>();
        private readonly VisualElement cards = new VisualElement();
        private readonly VisualElement summary = new VisualElement();
        private readonly Label noResults = new Label("No matching fields. Try another search.");
        private readonly ToolbarSearchField search = new ToolbarSearchField();
        private readonly Dictionary<Foldout, bool> previousExpansion = new Dictionary<Foldout, bool>();
        private ScrollView scroll;

        private sealed class Section
        {
            public Foldout Card;
            public string Title;
            public readonly List<(VisualElement element, string text)> Fields = new List<(VisualElement, string)>();
        }

        public ItemConfigInspectorView(SerializedObject serializedObject, bool inLibrary)
        {
            data = serializedObject;
            AddToClassList("item-editor");
            EnableInClassList("item-editor--light", !EditorGUIUtility.isProSkin);
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StylePath);
            if (stylesheet != null) styleSheets.Add(stylesheet);

            var header = new VisualElement();
            header.AddToClassList("item-header");
            var title = new Label(data.isEditingMultipleObjects
                ? $"{data.targetObjects.Length} selected items" : data.targetObject.name);
            title.AddToClassList("item-title");
            header.Add(title);
            var type = new Label(ObjectNames.NicifyVariableName(data.targetObject.GetType().Name));
            type.AddToClassList("item-subtitle");
            header.Add(type);
            var actions = new VisualElement();
            actions.AddToClassList("item-actions");
            if (!inLibrary)
                actions.Add(new Button(() => ItemLibraryWindow.Open(data.targetObject)) { text = "Open Item Library" });
            actions.Add(new Button(() => EditorGUIUtility.PingObject(data.targetObject)) { text = "Find asset" });
            header.Add(actions);
            Add(header);
            summary.AddToClassList("item-summary");
            Add(summary);

            var toolbar = new VisualElement();
            toolbar.AddToClassList("item-field-tools");
            search.tooltip = "Search all sections, field names, and property paths";
            search.AddToClassList("item-search");
            search.RegisterValueChangedCallback(evt => Filter(evt.newValue));
            toolbar.Add(search);
            var expansion = new VisualElement();
            expansion.AddToClassList("item-actions");
            expansion.Add(new Button(() => SetExpanded(true)) { text = "Expand" });
            expansion.Add(new Button(() => SetExpanded(false)) { text = "Collapse" });
            toolbar.Add(expansion);
            Add(toolbar);

            var navigation = new VisualElement();
            navigation.AddToClassList("item-navigation");
            Add(navigation);
            if (inLibrary)
            {
                style.flexGrow = 1;
                style.minHeight = 0;
                scroll = new ScrollView(ScrollViewMode.Vertical);
                scroll.style.flexGrow = 1;
                scroll.style.minWidth = 0;
                scroll.style.minHeight = 0;
                scroll.Add(cards);
                Add(scroll);
            }
            else Add(cards);

            var iterator = data.GetIterator();
            if (iterator.NextVisible(true))
            {
                do
                {
                    if (iterator.name == "m_Script") continue;
                    // The projectile is its own section instead of being buried inside Weapon.
                    AddSection(iterator.Copy(), iterator.name == "Weapon" ? "Bullet" : null);
                    if (iterator.name == "Weapon")
                    {
                        var bullet = iterator.FindPropertyRelative("Bullet");
                        if (bullet != null) AddSection(bullet, null, "Projectile");
                    }
                } while (iterator.NextVisible(false));
            }
            foreach (var section in sections)
            {
                var button = new Button(() =>
                {
                    search.value = "";
                    section.Card.value = true;
                    schedule.Execute(() => (scroll ?? GetFirstAncestorOfType<ScrollView>())?.ScrollTo(section.Card));
                }) { text = section.Title };
                navigation.Add(button);
            }
            noResults.AddToClassList("item-empty");
            noResults.style.display = DisplayStyle.None;
            cards.Add(noResults);
            UpdateSummary();
            this.TrackSerializedObjectValue(data, _ => UpdateSummary());
        }

        private void AddSection(SerializedProperty property, string excludedChild = null, string title = null)
        {
            var section = new Section
            {
                Title = title ?? SectionTitle(property.name),
                Card = new Foldout { value = true }
            };
            section.Card.text = section.Title;
            section.Card.viewDataKey = data.targetObject.GetType().Name + "." + property.propertyPath;
            section.Card.AddToClassList("item-card");
            if (property.propertyType == SerializedPropertyType.Generic && !property.isArray)
                AddChildren(section, property, "", excludedChild);
            else AddField(section, property, "");
            cards.Add(section.Card);
            sections.Add(section);
        }

        private void AddChildren(Section section, SerializedProperty parent, string prefix, string excludedChild = null)
        {
            var child = parent.Copy();
            var end = parent.GetEndProperty();
            if (!child.NextVisible(true)) return;
            while (!SerializedProperty.EqualContents(child, end) && child.depth > parent.depth)
            {
                if (child.name != excludedChild) AddField(section, child.Copy(), prefix);
                if (!child.NextVisible(false)) break;
            }
        }

        private void AddField(Section section, SerializedProperty property, string prefix)
        {
            // Bind directly to the prefab reference: the existing asset wrappers have IMGUI-only drawers.
            var prefab = property.propertyType == SerializedPropertyType.Generic
                ? property.FindPropertyRelative("asset.Prefab") ?? property.FindPropertyRelative("Prefab") : null;
            if (prefab != null)
            {
                AddReference(section, prefab, prefix + "Prefab");
                return;
            }
            if (property.propertyType == SerializedPropertyType.Generic && !property.isArray)
            {
                AddChildren(section, property, prefix + property.displayName + " / ");
                return;
            }
            var label = prefix + FieldTitle(property);
            var field = new PropertyField(property, label) { tooltip = property.propertyPath };
            field.AddToClassList("item-field");
            section.Card.Add(field);
            section.Fields.Add((field, section.Title + " " + label + " " + property.propertyPath));
        }

        private void AddReference(Section section, SerializedProperty property, string label)
        {
            var row = new VisualElement();
            row.AddToClassList("item-reference");
            var field = new ObjectField(label)
            {
                objectType = typeof(GameObject), allowSceneObjects = false,
                bindingPath = property.propertyPath,
                tooltip = "Drag a prefab from the Project window."
            };
            field.AddToClassList("unity-base-field__aligned");
            field.style.flexGrow = 1;
            row.Add(field);
            var path = property.propertyPath;
            var ping = new Button(() =>
            {
                var reference = data.FindProperty(path);
                if (reference != null && !reference.hasMultipleDifferentValues)
                    EditorGUIUtility.PingObject(reference.objectReferenceValue);
            }) { text = "Find", tooltip = "Highlight the assigned prefab in the Project window" };
            row.Add(ping);
            row.TrackPropertyValue(property, current =>
                ping.SetEnabled(!current.hasMultipleDifferentValues && current.objectReferenceValue != null));
            ping.SetEnabled(!property.hasMultipleDifferentValues && property.objectReferenceValue != null);
            section.Card.Add(row);
            section.Fields.Add((row, section.Title + " " + label + " " + path));
        }

        private void Filter(string query)
        {
            query = query.Trim();
            bool searching = query.Length > 0;
            int matches = 0;
            foreach (var section in sections)
            {
                if (searching && !previousExpansion.ContainsKey(section.Card))
                    previousExpansion.Add(section.Card, section.Card.value);
                bool visible = !searching;
                foreach (var field in section.Fields)
                {
                    bool show = !searching || field.text.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0;
                    field.element.style.display = show ? DisplayStyle.Flex : DisplayStyle.None;
                    visible |= show;
                }
                section.Card.style.display = visible ? DisplayStyle.Flex : DisplayStyle.None;
                if (searching && visible) section.Card.value = true;
                if (!searching && previousExpansion.TryGetValue(section.Card, out bool expanded))
                    section.Card.value = expanded;
                if (visible) matches++;
            }
            if (!searching) previousExpansion.Clear();
            noResults.style.display = matches == 0 ? DisplayStyle.Flex : DisplayStyle.None;
        }

        private void SetExpanded(bool value)
        {
            search.value = "";
            foreach (var section in sections) section.Card.value = value;
        }

        private void UpdateSummary()
        {
            if (data.targetObject == null) return;
            summary.Clear();
            AddStat("Healing", "Amount", " HP");
            AddStat("Cooldown", "Weapon.Cooldown", " s");
            AddStat("Range", "Weapon.Range", "");
            AddStat("Duration", "Lifetime.LifeTime", " s");
            AddStat("Magazine", "Clip.BulletsInClip", "", true);
        }

        private void AddStat(string title, string path, string unit, bool zeroIsUnlimited = false)
        {
            var property = data.FindProperty(path);
            if (property == null) return;
            float number = property.propertyType == SerializedPropertyType.Integer ? property.intValue : property.floatValue;
            var value = property.hasMultipleDifferentValues ? "Mixed"
                : zeroIsUnlimited && number == 0 ? "Unlimited" : number.ToString("0.##") + unit;
            var tile = new VisualElement();
            tile.AddToClassList("item-stat");
            var label = new Label(title);
            label.AddToClassList("item-subtitle");
            tile.Add(label);
            var valueLabel = new Label(value);
            valueLabel.AddToClassList("item-stat-value");
            tile.Add(valueLabel);
            summary.Add(tile);
        }

        private static string SectionTitle(string name)
        {
            switch (name)
            {
                case "Item": return "Identity";
                case "Weapon": return "Weapon";
                case "Lifetime": return "Duration";
                case "TargetDetection": return "Targeting";
                case "Scattering": return "Spread";
                case "Clip": return "Magazine";
                case "Bullet": return "Rain behavior";
                case "Amount": return "Healing";
                default: return ObjectNames.NicifyVariableName(name);
            }
        }

        private static string FieldTitle(SerializedProperty property)
        {
            switch (property.name)
            {
                case "LifeTime": return "Lifetime (s)";
                case "Cooldown": return "Shot cooldown (s)";
                case "ClipCooldown": return "Reload time (s)";
                case "BulletsInClip": return "Magazine size (0 = unlimited)";
                case "Angle": return "Spread angle (degrees)";
                case "AngleToAttack": return "Attack angle (degrees)";
                case "Amount": return "Health restored";
                default: return property.displayName;
            }
        }
    }
}
