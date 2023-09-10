using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace SoCreator
{
    public class SettingsProvider : UnityEditor.SettingsProvider
    {
        public const string k_AllAssemblies     = nameof(SoCreator) + ".AllAssemblies";
        public const string k_Width             = nameof(SoCreator) + ".Width";
        public const string k_MaxItems          = nameof(SoCreator) + ".MaxItems";
        public const string k_ShowNamespace     = nameof(SoCreator) + ".ShowNamespace";
        public const string k_KeepSearchText    = nameof(SoCreator) + ".KeepSearchText";
        public const string k_FormatDefaultName = nameof(SoCreator) + ".FormatDefaultName";
        public const string k_SearchText        = nameof(SoCreator) + ".SearchText";
        public const string k_PrefsFile         = nameof(SoCreator) + "Prefs.json";
        public const string k_PrefsPath         = "ProjectSettings\\" + k_PrefsFile;
        
        public static EditorOption s_IgnoreSubTypeFolder = new EditorOption("IgnoreSubTypeFolder");
        public static EditorOption s_RequireMonoScript = new EditorOption("RequireMonoScript");

        public const bool k_AllAssambliesDefault     = false;
        public const bool k_ShowNamespaceDefault     = true;
        public const bool k_KeepSearchTextDefault    = false;
        public const bool k_FormatDefaultNameDefault = false;
        public const int  k_WeightDefault            = 320;
        public const int  k_MaxItemsDefault          = 70;

        public static List<AssemblyDefinitionAsset> s_Assemblies;
        public static List<TypePath>                s_TypeFolders;
        
        private ReorderableList _assemblesList;
        private ReorderableList _foldersList;

        // =======================================================================
        [Serializable]
        private class JsonWrapper
        {
            public List<string>                   Assemblies;
            public DictionaryData<string, string> DefaultPath;
            
            // =======================================================================
            [Serializable]
            public class DictionaryData<TKey, TValue>
            {
                public List<TKey>   Keys;
                public List<TValue> Values;
                
                public IEnumerable<KeyValuePair<TKey, TValue>> Enumerate()
                {
                    if (Keys == null || Values == null)
                        yield break; 
                            
                    for (var n = 0; n < Keys.Count; n++)
                        yield return new KeyValuePair<TKey, TValue>(Keys[n], Values[n]);
                }

                public DictionaryData() 
                    : this(new List<TKey>(), new List<TValue>())
                {
                }
                
                public DictionaryData(List<TKey> keys, List<TValue> values)
                {
                    Keys   = keys;
                    Values = values;
                }
                
                public DictionaryData(IEnumerable<KeyValuePair<TKey, TValue>> data)
                {
                    var pairs = data as KeyValuePair<TKey, TValue>[] ?? data.ToArray();
                    Keys   = pairs.Select(n => n.Key).ToList();
                    Values = pairs.Select(n => n.Value).ToList();
                }
            }
        }

        [Serializable]
        public class TypePath
        {
            public Type         Type;
            public DefaultAsset Path;
            
            // =======================================================================
            public TypePath(Type type, DefaultAsset path)
            {
                Type  = type;
                Path = path;
            }
        }
        
        [Serializable]
        public class EditorOption
        {
            public string _key;
            public object _val;

            // =======================================================================
            public EditorOption(string key)
            {
                _key = key;
            }

            public void Setup<T>(T def)
            {
                if (HasPrefs() == false)
                    Write(def);
                
                _val = Read<T>(def);
            }
            
            public bool HasPrefs() => EditorPrefs.HasKey(_key);
            
            public T Get<T>()
            {
                return (T)_val;
            }
            
            public T Read<T>(T fallOff = default)
            {
                try
                {
                    var type = typeof(T);
                    
                    if (type == typeof(bool))
                        return (T)(object)EditorPrefs.GetBool(_key);
                    if (type == typeof(int))
                        return (T)(object)EditorPrefs.GetInt(_key);
                    if (type == typeof(float))
                        return (T)(object)EditorPrefs.GetFloat(_key);
                    if (type == typeof(string))
                        return (T)(object)EditorPrefs.GetString(_key);
                    
                    return JsonUtility.FromJson<T>(EditorPrefs.GetString(_key));
                }
                catch
                {
                    return fallOff;
                }
            }
            
            public void Write<T>(T val)
            {
                var type = typeof(T);
                _val = val;
                
                if (type == typeof(bool))
                    EditorPrefs.SetBool(_key, (bool)_val);
                else
                if (type == typeof(int))
                    EditorPrefs.SetInt(_key, (int)_val);
                else
                if (type == typeof(string))
                    EditorPrefs.SetString(_key, (string)_val);
                else
                if (type == typeof(float))
                    EditorPrefs.SetFloat(_key, (float)_val);
                else
                    EditorPrefs.SetString(_key, JsonUtility.ToJson(val));
            }
            
            public void OnGui<T>(Func<T, T> draw)
            {
                EditorGUI.BeginChangeCheck();
                
                var val = draw(Get<T>());
                
                if (EditorGUI.EndChangeCheck())
                    Write(val);
            }
        }
        
        // =======================================================================
        public SettingsProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) : base(path, scopes, keywords)
        {
        }
        
        [InitializeOnLoadMethod]
        private static void InitializeOnLoad()
        {
            s_Assemblies   = new List<AssemblyDefinitionAsset>();
            s_TypeFolders = new List<TypePath>();
            
            if (File.Exists(k_PrefsPath))
            {
                using var file = File.OpenText(k_PrefsPath);
                var       data = JsonUtility.FromJson<JsonWrapper>(file.ReadToEnd());
                
                s_Assemblies = data.Assemblies
                                   .Select(guid => AssetDatabase.LoadAssetAtPath<AssemblyDefinitionAsset>(AssetDatabase.GUIDToAssetPath(guid)))
                                   .ToList();
                
                s_TypeFolders = data.DefaultPath
                                     .Enumerate()
                                     .Select(n => new TypePath(
                                                 Type.GetType(n.Key) ?? typeof(None),
                                                 AssetDatabase.LoadAssetAtPath<DefaultAsset>(AssetDatabase.GUIDToAssetPath(n.Value))))
                                     .ToList();
            }
            
            if (!EditorPrefs.HasKey(k_AllAssemblies))
                EditorPrefs.SetBool(k_AllAssemblies, k_AllAssambliesDefault);
            
            if (!EditorPrefs.HasKey(k_ShowNamespace))
                EditorPrefs.SetBool(k_ShowNamespace, k_ShowNamespaceDefault);
            
            if (!EditorPrefs.HasKey(k_KeepSearchText))
                EditorPrefs.SetBool(k_KeepSearchText, k_KeepSearchTextDefault);
                
            if (!EditorPrefs.HasKey(k_FormatDefaultName))
                EditorPrefs.SetBool(k_KeepSearchText, k_FormatDefaultNameDefault);
            
            if (!EditorPrefs.HasKey(k_Width))
                EditorPrefs.SetInt(k_Width, k_WeightDefault);
            
            if (!EditorPrefs.HasKey(k_MaxItems))
                EditorPrefs.SetInt(k_MaxItems, k_MaxItemsDefault);
            
            if (!EditorPrefs.HasKey(k_SearchText))
                EditorPrefs.SetString(k_SearchText, string.Empty);
            
            s_IgnoreSubTypeFolder.Setup(true);
            s_RequireMonoScript.Setup(false);
            
        }

        private static bool _advansed;
        public override void OnGUI(string searchContext)
        {
            //EditorGUILayout.ObjectField(null, typeof(AssemblyDefinitionAsset), false);
            EditorGUI.BeginChangeCheck();
            var allAssambles      = EditorGUILayout.Toggle(new GUIContent("All Assemblies", "Search in all assemblies by default"), EditorPrefs.GetBool(k_AllAssemblies));
            var showNamespace     = EditorGUILayout.Toggle(new GUIContent("Full Names", "Show namespace in type name"), EditorPrefs.GetBool(k_ShowNamespace));
            var keepSearchText    = EditorGUILayout.Toggle(new GUIContent("Keep Search Text", "Keep previously entered search text"), EditorPrefs.GetBool(k_KeepSearchText));
            s_RequireMonoScript.OnGui<bool>(val => EditorGUILayout.Toggle(new GUIContent("Require MonoScript", "Only show So types which have an associated MonoScript"), val));
            
            using (new EditorGUILayout.VerticalScope("Box"))
            {
                _advansed = EditorGUILayout.Foldout(_advansed, new GUIContent("Advanced"), true);
                if (_advansed)
                {
                    EditorGUI.indentLevel ++;
                    var formatDefaultName = EditorGUILayout.Toggle(new GUIContent("Nicify default name", "Nicify default So name, for example _SoMoveData will be looked as So Move Data"), EditorPrefs.GetBool(k_FormatDefaultName));
                    s_IgnoreSubTypeFolder.OnGui<bool>(val => EditorGUILayout.Toggle(new GUIContent("Ignore subfolder path", "if So was created inside a subdirectory of type path, then type path relocation will be ignored"), val));
                    var width    = EditorGUILayout.IntField(new GUIContent("Width", "Window width"), EditorPrefs.GetInt(k_Width));
                    var maxItems = EditorGUILayout.IntField(new GUIContent("Max Items", "Max elements in popup window"), EditorPrefs.GetInt(k_MaxItems));
                    EditorGUI.indentLevel --;
                    EditorPrefs.SetBool(k_FormatDefaultName, formatDefaultName);
                    EditorPrefs.SetInt(k_Width, Mathf.Max(width, PickerWindow.k_Width));
                    EditorPrefs.SetInt(k_MaxItems, Mathf.Max(maxItems, 7));
                }
            }
            
            EditorGUILayout.Space(7);
            _getAssembliesList().DoLayoutList();
            _getFoldersList().DoLayoutList();
            
            if (EditorGUI.EndChangeCheck())
            {
                EditorPrefs.SetBool(k_AllAssemblies, allAssambles);
                EditorPrefs.SetBool(k_ShowNamespace, showNamespace);
                EditorPrefs.SetBool(k_KeepSearchText, keepSearchText);
            }
        }

        private ReorderableList _getAssembliesList()
        {
            if (_assemblesList != null)
                return _assemblesList;
            
            _assemblesList = new ReorderableList(s_Assemblies, typeof(AssemblyDefinitionAsset), true, true, true, true);
            _assemblesList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                EditorGUI.BeginChangeCheck();
                var asm = EditorGUI.ObjectField(rect, GUIContent.none, s_Assemblies[index], typeof(AssemblyDefinitionAsset), false);
                if (EditorGUI.EndChangeCheck())
                {
                    s_Assemblies[index] = (AssemblyDefinitionAsset)asm;
                    _saveProjectPrefs();
                }
            };
            _assemblesList.elementHeight = EditorGUIUtility.singleLineHeight;
            _assemblesList.onRemoveCallback = list =>
            {
                s_Assemblies.RemoveAt(list.index);
                _saveProjectPrefs();
            };
            _assemblesList.onAddCallback = list =>
            {
                s_Assemblies.Add(null);
            };
            _assemblesList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, new GUIContent("Include assemblies", "Which assemblies to include in the search"));
            };
            
            return _assemblesList;
        }
        
        private ReorderableList _getFoldersList()
        {
            if (_foldersList != null)
                return _foldersList;
            
            _foldersList = new ReorderableList(s_TypeFolders, typeof(TypePath), true, true, true, true);
            _foldersList.drawElementCallback = (rect, index, isActive, isFocused) =>
            {
                var element = s_TypeFolders[index];
                
                var typeRect = new Rect(rect.position, new Vector2(rect.size.x * .5f - EditorGUIUtility.standardVerticalSpacing, rect.size.y));
                var pathRect = new Rect(rect.position + new Vector2(rect.size.x * .5f, 0f), new Vector2(rect.size.x * .5f, rect.size.y));
                
                if (GUI.Button(typeRect, element.Type?.FullName ?? "None", EditorStyles.popup))
                {
                    var types = SoCreator.GetSoTypes(true, type => true);
                    
                    var showNamespace  = EditorPrefs.GetBool(SettingsProvider.k_ShowNamespace);
                    var wndWidth       = (float)EditorPrefs.GetInt(SettingsProvider.k_Width);
                    var wndMaxItems    =  EditorPrefs.GetInt(SettingsProvider.k_MaxItems);
                    
                    PickerWindow.Show(picked =>
                                      {
                                          var pickedType   = (Type)picked;
                                          element.Type = pickedType;
                                          _saveProjectPrefs();
                                      }, null, types, 0, s => new GUIContent(showNamespace ? s.FullName : s.Name), 
                                      title: "ScriptableObject Type", 
                                      firstClickTrigger: true, 
                                      width: wndWidth, 
                                      maxElements: wndMaxItems,
                                      searchText: string.Empty);
                }

                EditorGUI.BeginChangeCheck();
                var folder        = EditorGUI.ObjectField(pathRect, GUIContent.none, element.Path, typeof(DefaultAsset), false);
                if (EditorGUI.EndChangeCheck())
                {
                    // ignore non directory files
                    if (folder != null && File.GetAttributes(AssetDatabase.GetAssetPath(folder)).HasFlag(FileAttributes.Directory) == false)
                        folder = null;
                     
                    element.Path = (DefaultAsset)folder;
                    _saveProjectPrefs();
                }
            };
            _foldersList.elementHeight = EditorGUIUtility.singleLineHeight;
            _foldersList.onRemoveCallback = list =>
            {
                s_TypeFolders.RemoveAt(list.index);
                _saveProjectPrefs();
            };
            _foldersList.onAddCallback = list =>
            {
                s_TypeFolders.Add(new TypePath(typeof(None), null));
            };
            _foldersList.drawHeaderCallback = rect =>
            {
                EditorGUI.LabelField(rect, new GUIContent("Type paths", "Default paths for Script Objects created via shortcut"));
            };
            
            return _foldersList;
        }
        
        
        private void _saveProjectPrefs()
        {
            var json = new JsonWrapper()
            {
                Assemblies = s_Assemblies
                             .Select(asset => AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(asset)))
                             .Where(n => n.Empty() == false)
                             .Select(n => n.ToString())
                             .ToList(),
                
                DefaultPath = new JsonWrapper.DictionaryData<string, string>(_defaultPathsData())
            };
            
            File.WriteAllText(k_PrefsPath, JsonUtility.ToJson(json));

        
            // -----------------------------------------------------------------------
            IEnumerable<KeyValuePair<string, string>> _defaultPathsData()
            {
                var data = s_TypeFolders.ToArray();
                for (var n = 0; n < data.Length; n++)
                {
                    var type = data[n].Type;
                    var path = data[n].Path;
                    
                    if (type == null)
                        type = typeof(None);
                    
                    //if (path == null)
                    //    continue;

                    var pathGuid = AssetDatabase.GUIDFromAssetPath(AssetDatabase.GetAssetPath(path));
                    //if (pathGuid.Empty())
                    //    continue;
                    
                    yield return new KeyValuePair<string, string>(type.AssemblyQualifiedName, pathGuid.ToString());
                }
            }
            
        }
        
        [SettingsProvider]
        public static UnityEditor.SettingsProvider CreateMyCustomSettingsProvider()
        {
            var provider = new SettingsProvider("Preferences/SoCreator", SettingsScope.User);

            // Automatically extract all keywords from the Styles.
            //provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            return provider;
        }
    }
    
    public class None : ScriptableObject { }
}