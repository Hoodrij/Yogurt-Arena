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
        [Header("Agents")]
        public AgentData Player;
        public AgentData ChargeEnemy;
        public OvermindData Overmind;
        [Header("Weapon")]
        public RifleData Rifle;
        public RainData Rain;
        public ChargeData Charge;
        [Header("Items")]
        public ItemSpotData ItemSpot;
        public HealingPotionData HealingPotion;
    }
}