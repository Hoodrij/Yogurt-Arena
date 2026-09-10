using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Yogurt.Arena.Editor
{
    public sealed class ItemLibraryWindow : EditorWindow
    {
        private static readonly (string label, Type type)[] ConfigTypes =
        {
            ("Boomer", typeof(BoomerWeaponConfig)),
            ("Charger", typeof(ChargerWeaponConfig)),
            ("Healing potion", typeof(HealingPotionConfig)),
            ("Rain", typeof(RainConfig)),
            ("Rifle / Shotgun", typeof(RifleConfig))
        };

        [SerializeField] private string query = "";
        [SerializeField] private string kind = "All types";
        [SerializeField] private List<string> selectedGuids = new List<string>();
        private readonly List<ScriptableObject> assets = new List<ScriptableObject>();
        private readonly List<ScriptableObject> visibleAssets = new List<ScriptableObject>();
        private ListView list;
        private Label count;
        private VisualElement details;
        private SerializedObject editing;
        private ToolbarSearchField search;
        private DropdownField typeFilter;
        private Button duplicate;
        private Button save;
        private bool restoringSelection;

        [MenuItem("Tools/Yogurt/Item Library")]
        public static void ShowWindow() => Open(null);

        public static void Open(Object asset)
        {
            var window = GetWindow<ItemLibraryWindow>();
            window.titleContent = new GUIContent("Item Library");
            window.minSize = new Vector2(680, 420);
            if (asset != null)
            {
                window.query = "";
                window.kind = "All types";
                window.selectedGuids = new List<string> { AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset)) };
                window.search?.SetValueWithoutNotify("");
                window.typeFilter?.SetValueWithoutNotify("All types");
                window.RefreshAssets();
            }
            window.Show();
        }

        private void OnEnable() => Undo.undoRedoPerformed += OnUndoRedo;

        private void OnDisable()
        {
            Undo.undoRedoPerformed -= OnUndoRedo;
            ReleaseEditor();
        }

        public void CreateGUI()
        {
            ReleaseEditor();
            rootVisualElement.Clear();
            rootVisualElement.AddToClassList("item-library");
            rootVisualElement.EnableInClassList("item-editor--light", !EditorGUIUtility.isProSkin);
            var stylesheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(ItemConfigInspectorView.StylePath);
            if (stylesheet != null) rootVisualElement.styleSheets.Add(stylesheet);

            var toolbar = new Toolbar();
            toolbar.Add(new ToolbarButton(ShowCreateMenu) { text = "Create item" });
            duplicate = new ToolbarButton(DuplicateSelected) { text = "Duplicate" };
            toolbar.Add(duplicate);
            save = new ToolbarButton(SaveSelected) { text = "Save selected" };
            toolbar.Add(save);
            var spacer = new VisualElement();
            spacer.style.flexGrow = 1;
            toolbar.Add(spacer);
            toolbar.Add(new ToolbarButton(RefreshAssets) { text = "Refresh" });
            rootVisualElement.Add(toolbar);

            var split = new TwoPaneSplitView(0, 235, TwoPaneSplitViewOrientation.Horizontal)
            {
                viewDataKey = "item-library-split"
            };
            split.style.flexGrow = 1;
            rootVisualElement.Add(split);
            var browser = new VisualElement();
            browser.AddToClassList("item-browser");
            browser.style.minWidth = 190;
            split.Add(browser);

            search = new ToolbarSearchField { value = query, tooltip = "Search item names, types, and tags" };
            search.AddToClassList("item-search");
            search.RegisterValueChangedCallback(evt => { query = evt.newValue; ApplyFilter(); });
            browser.Add(search);
            var choices = new List<string> { "All types" };
            choices.AddRange(ConfigTypes.Select(entry => entry.label));
            if (!choices.Contains(kind)) kind = choices[0];
            typeFilter = new DropdownField(choices, choices.IndexOf(kind));
            typeFilter.RegisterValueChangedCallback(evt => { kind = evt.newValue; ApplyFilter(); });
            browser.Add(typeFilter);
            count = new Label();
            count.AddToClassList("item-browser-count");
            browser.Add(count);
            list = new ListView
            {
                itemsSource = visibleAssets,
                fixedItemHeight = 58,
                selectionType = SelectionType.Multiple,
                makeItem = MakeRow,
                bindItem = BindRow
            };
            list.style.flexGrow = 1;
            list.selectionChanged += OnSelectionChanged;
            browser.Add(list);
            var hint = new Label("Ctrl / Shift to select several items of the same type.");
            hint.AddToClassList("item-browser-hint");
            browser.Add(hint);
            details = new VisualElement();
            details.AddToClassList("item-details");
            details.style.flexGrow = 1;
            details.style.minWidth = 340;
            split.Add(details);
            RefreshAssets();
        }

        private static VisualElement MakeRow()
        {
            var row = new VisualElement();
            row.AddToClassList("item-browser-row");
            var title = new Label { name = "name" };
            title.AddToClassList("item-browser-name");
            row.Add(title);
            var subtitle = new Label { name = "kind" };
            subtitle.AddToClassList("item-subtitle");
            row.Add(subtitle);
            return row;
        }

        private void BindRow(VisualElement row, int index)
        {
            var asset = visibleAssets[index];
            row.Q<Label>("name").text = asset != null ? asset.name : "Missing asset";
            row.Q<Label>("kind").text = asset != null ? Describe(asset) : "";
            row.tooltip = asset != null ? AssetDatabase.GetAssetPath(asset) : "";
        }

        private static string KindOf(ScriptableObject asset) =>
            ConfigTypes.First(entry => entry.type == asset.GetType()).label;

        private static string Describe(ScriptableObject asset)
        {
            var item = (ItemConfig)asset.GetType().GetField("Item").GetValue(asset);
            return $"{KindOf(asset)}  ·  {item.Type}  ·  {item.Tags}";
        }

        private void OnProjectChange() => RefreshAssets();

        private void OnUndoRedo()
        {
            if (list != null) ApplyFilter();
        }

        private void RefreshAssets()
        {
            if (list == null) return;
            assets.Clear();
            foreach (var entry in ConfigTypes)
            {
                foreach (string guid in AssetDatabase.FindAssets("t:" + entry.type.Name, new[] { "Assets" }))
                {
                    var asset = AssetDatabase.LoadAssetAtPath<ScriptableObject>(AssetDatabase.GUIDToAssetPath(guid));
                    if (asset != null && asset.GetType() == entry.type && !assets.Contains(asset)) assets.Add(asset);
                }
            }
            assets.Sort((a, b) => string.Compare(a.name, b.name, StringComparison.OrdinalIgnoreCase));
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            visibleAssets.Clear();
            string term = query.Trim();
            visibleAssets.AddRange(assets.Where(asset => asset != null
                && (kind == "All types" || KindOf(asset) == kind)
                && (asset.name + " " + Describe(asset)).IndexOf(term, StringComparison.OrdinalIgnoreCase) >= 0));
            count.text = $"{visibleAssets.Count} of {assets.Count} items";
            restoringSelection = true;
            list.Rebuild();
            var indices = new List<int>();
            for (int i = 0; i < visibleAssets.Count; i++)
                if (selectedGuids.Contains(AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(visibleAssets[i])))) indices.Add(i);
            list.SetSelectionWithoutNotify(indices);
            restoringSelection = false;
            ShowDetails(indices.Select(i => visibleAssets[i]).ToArray());
        }

        private void OnSelectionChanged(IEnumerable<object> selection)
        {
            if (restoringSelection) return;
            var selected = selection.OfType<ScriptableObject>().ToArray();
            selectedGuids = selected.Select(asset => AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(asset))).ToList();
            ShowDetails(selected);
        }

        private void ShowDetails(ScriptableObject[] selected)
        {
            duplicate.SetEnabled(selected.Length == 1);
            save.SetEnabled(selected.Length > 0);
            if (editing != null && editing.targetObjects.SequenceEqual(selected.Cast<Object>())) return;
            ReleaseEditor();
            details.Clear();
            if (selected.Length == 0)
            {
                var empty = new Label(visibleAssets.Count == 0
                    ? "No items match these filters. Clear the search or create an item."
                    : "Select an item to edit its settings.");
                empty.AddToClassList("item-empty");
                details.Add(empty);
                return;
            }
            if (selected.Select(asset => asset.GetType()).Distinct().Count() > 1)
            {
                details.Add(new HelpBox("Select items of the same config type to edit their values together.", HelpBoxMessageType.Info));
                return;
            }
            editing = new SerializedObject(selected.Cast<Object>().ToArray());
            var view = new ItemConfigInspectorView(editing, true);
            details.Add(view);
            view.Bind(editing);
            view.RegisterCallback<SerializedPropertyChangeEvent>(_ => list.RefreshItems());
        }

        private void ReleaseEditor()
        {
            details?.Unbind();
            editing?.Dispose();
            editing = null;
        }

        private void ShowCreateMenu()
        {
            var menu = new GenericMenu();
            foreach (var entry in ConfigTypes)
                menu.AddItem(new GUIContent(entry.label), false, () => CreateItem(entry.type));
            menu.ShowAsContext();
        }

        private void CreateItem(Type type)
        {
            string path = EditorUtility.SaveFilePanelInProject("Create item config", "New " + ObjectNames.NicifyVariableName(type.Name),
                "asset", "Choose a name for the new item config.", "Assets/Resources/Config/Items");
            if (string.IsNullOrEmpty(path)) return;
            path = AssetDatabase.GenerateUniqueAssetPath(path);
            var asset = CreateInstance(type);
            AssetDatabase.CreateAsset(asset, path);
            Open(asset);
            EditorGUIUtility.PingObject(asset);
        }

        private void DuplicateSelected()
        {
            var source = list.selectedItems.OfType<ScriptableObject>().SingleOrDefault();
            if (source == null) return;
            string sourcePath = AssetDatabase.GetAssetPath(source);
            string destination = EditorUtility.SaveFilePanelInProject("Duplicate item config", source.name + " Copy", "asset",
                "Choose a name for the copy.", System.IO.Path.GetDirectoryName(sourcePath));
            if (string.IsNullOrEmpty(destination)) return;
            destination = AssetDatabase.GenerateUniqueAssetPath(destination);
            // Instantiate the in-memory asset so unsaved Inspector edits are included in the copy.
            var copy = Instantiate(source);
            copy.name = System.IO.Path.GetFileNameWithoutExtension(destination);
            AssetDatabase.CreateAsset(copy, destination);
            Open(copy);
        }

        private void SaveSelected()
        {
            foreach (var asset in list.selectedItems.OfType<ScriptableObject>())
                AssetDatabase.SaveAssetIfDirty(asset);
            ShowNotification(new GUIContent("Selected items saved"));
        }
    }
}
