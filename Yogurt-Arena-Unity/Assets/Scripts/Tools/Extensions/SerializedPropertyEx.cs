using System.Collections.Generic;
using System.Reflection;
using UnityEditor;

namespace Yogurt.Arena
{
    public static class SerializedPropertyEx
    {
        public static T GetValue<T>(this SerializedProperty property)
        {
            return (T) property.GetValue();
        }
        
        public static object GetValue(this SerializedProperty property)
        {
            object obj = property.serializedObject.targetObject;

            foreach(var path in property.propertyPath.Split('.'))
            {
                var type = obj.GetType();
                FieldInfo field = type.GetField(path);
                obj = field.GetValue(obj);
            }
            return obj;
        }
 
        public static void SetValue(this SerializedProperty property, object val)
        {
            object obj = property.serializedObject.targetObject;
 
            List<KeyValuePair<FieldInfo, object>> list = new List<KeyValuePair<FieldInfo, object>>();

            foreach(var path in property.propertyPath.Split('.'))
            {
                var type = obj.GetType();
                FieldInfo field = type.GetField(path);
                list.Add(new KeyValuePair<FieldInfo, object>(field, obj));
                obj = field.GetValue(obj);
            }
 
            for(int i = list.Count - 1; i >= 0; --i)
            {
                list[i].Key.SetValue(list[i].Value, val);
                val = list[i].Value;
            }
        }
    }
}