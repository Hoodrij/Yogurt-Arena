using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject , IComponent
    {
        public InputData Input;
        public CameraData Camera;
        public BeaconData Beacon;
        public AgentsData Agent;
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
    
    [System.Serializable]
    public class AgentsData
    {
        public int NavMeshID = 0;
    
        public float MoveSpeed;
        public float MoveSmoothValue;
    }
}