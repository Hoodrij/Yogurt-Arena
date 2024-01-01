using System;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Yogurt
{
    public class ProfileResult : ScriptableObject
    {
        public string Result;

        public static void Start()
        {
            ProfileResult result = CreateInstance<ProfileResult>();
            Update();
            
            string path = AssetDatabase.GenerateUniqueAssetPath($"Assets/Profile/ProfileResult {DateTime.Now}.asset");
            AssetDatabase.CreateAsset(result, path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return;
            
            void Update()
            {
                StringBuilder sb = new();
                foreach (Profile profile in Profile.Cache.Values)
                {
                    sb.AppendLine(profile.ToString());
                    profile.Dispose();
                }
                Profile.Cache.Clear();

                result.Result = sb.ToString();
            }
        }
    }
}