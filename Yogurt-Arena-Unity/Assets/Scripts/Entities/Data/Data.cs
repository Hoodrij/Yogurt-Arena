using System;
using UnityEngine;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject , IComponent
    {
        public Assets Assets;

        public InputData Input;
        public CameraData Camera;
        public BeaconData Beacon;
        public AgentsData Agent;
        public OvermindData Overmind;
        public BulletData Bullet;
    }

    [Serializable]
    public class InputData
    {
        public float AccumulativeValue;
        public float DeAccumulativeValue;
        public float Sensitivity;
    }

    [Serializable]
    public class CameraData
    {
        public float SmoothValue;
    }

    [Serializable]
    public class BeaconData
    {
        public float SmoothValue;
        public float Elasticity;
    }
    
    [Serializable]
    public class AgentsData
    {
        public int NavMeshID = 0;
    
        public float MoveSpeed;
        public float MoveSmoothValue;
        public float FindTargetDistance;
    }
    
    [Serializable]
    public class OvermindData
    {
        public MinMaxInt WaveAgentsCount;
        public int MinimumAgents;
        public MinMax WavesDelay;
    }
    
    [Serializable]
    public class BulletData : IComponent
    {
        public LayerMask HitMask;
        public int Damage;
        public float LifeTime;
        public float Speed;
    }
}