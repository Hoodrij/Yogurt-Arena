using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject , IComponent
    {
        public CameraData Camera;
    }

    [System.Serializable]
    public class CameraData
    {
        public float SmoothValue = 0.1f;
        public float Sensitivity = 0.05f;
    }
}