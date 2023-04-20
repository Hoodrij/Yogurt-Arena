using UnityEngine;
using UnityEngine.Serialization;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject, IComponent
    {
        public Asset<World> World;
        public Asset<Level> Level;

        public InputData Input;
        public CameraData Camera;
        public BeaconData Beacon;
        public AgentData Player;
        public AgentData ChargeEnemy;
        public OvermindData Overmind;
        public WeaponData Rifle;
        public WeaponData Charge;
    }
}