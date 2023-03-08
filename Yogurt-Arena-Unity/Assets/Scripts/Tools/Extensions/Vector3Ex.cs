using UnityEngine;

namespace Yogurt.Arena
{
    public static class Vector3Ex
    {
        public static Vector3 WithX(this Vector3 v3, float x)
        {
            v3.Set(x, v3.y, v3.z);
            return v3;
        }

        public static Vector3 WithY(this Vector3 v3, float y)
        {
            v3.Set(v3.x, y, v3.z);
            return v3;
        }

        public static Vector3 WithZ(this Vector3 v3, float z)
        {
            v3.Set(v3.x, v3.y, z);
            return v3;
        }

        public static Vector3 ToV3XY(this Vector2 v2)
        {
            return new Vector3(v2.x, v2.y, 0);
        }

        public static Vector3 ToV3XZ(this Vector2 v2)
        {
            return new Vector3(v2.x, 0, v2.y);
        }
        
        public static Vector3 ClampMagnitude(this Vector3 v, float length)
        {
            if (v.magnitude > length)
            {
                v = v.normalized * length;    
            }
            
            return v;
        }
    }
}