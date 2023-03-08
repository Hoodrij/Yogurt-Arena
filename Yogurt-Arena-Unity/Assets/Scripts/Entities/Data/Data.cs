using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject , IComponent
    {
        public InputData Input;
        public CameraData Camera;
        public BeaconData Beacon;
    }

    [System.Serializable]
    public class InputData
    {
        public float SmoothValue;
        public float Sensitivity;
    }

    [System.Serializable]
    public class CameraData
    {
        public float SmoothValue;
    }

    [System.Serializable]
    public class BeaconData
    {
        public float SmoothValue;
        public float Elasticity;
    }
}