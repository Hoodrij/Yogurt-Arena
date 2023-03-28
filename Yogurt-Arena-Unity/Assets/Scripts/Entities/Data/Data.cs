using UnityEngine;
using UnityEngine.Serialization;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject , IComponent
    {
        public InputData Input;
        public CameraData Camera;
        public BeaconData Beacon;
        public AgentsData Agent;
        public OvermindData Overmind;
        public BulletData Bullet;
    }

    [System.Serializable]
    public class InputData
    {
        public float AccumulativeValue;
        [FormerlySerializedAs("AccumulativeBackValue")] public float DeAccumulativeValue;
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
        public float FindTargetDistance;
    }
    
    [System.Serializable]
    public class OvermindData
    {
        public int EnemiesCount;
    }
    
    [System.Serializable]
    public class BulletData : IComponent
    {
        public LayerMask HitMask;
        public float LifeTime;
        public float Speed;
    }
}