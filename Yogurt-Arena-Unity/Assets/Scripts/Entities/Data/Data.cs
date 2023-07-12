using UnityEngine;
using Yogurt.Arena.Components;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Data : ScriptableObject, IComponent
    {
        public Asset<World> World;
        public Asset<Level> Level;

        public UIData UIData;
        public InputData Input;
        public CameraData Camera;
        public BeaconData Beacon;
        public AgentData Player;
        public AgentData ChargeEnemy;
        public OvermindData Overmind;
        public RifleData Rifle;
        public RainData Rain;
        public WeaponData Charge;
        public ItemSpotData ItemSpot;
        public HealingPotionData HealingPotion;
    }
}