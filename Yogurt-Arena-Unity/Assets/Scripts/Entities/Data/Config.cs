using UnityEngine;
using Yogurt.Arena.Components;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Config : ScriptableObject, IComponent
    {
        public Asset<World> World;
        public Asset<Level> Level;

        public UIConfig UI;
        public InputConfig Input;
        public CameraConfig Camera;
        public BeaconConfig Beacon;
        
        [Header("Agents")]
        public AgentConfig Player;
        public AgentConfig ChargeEnemy;
        public OvermindConfig Overmind;
        
        [Header("Weapon")]
        public RifleConfig Rifle;
        public RainConfig Rain;
        public ChargeConfig Charge;
        
        [Header("Items")]
        public ItemSpotConfig ItemSpot;
        public HealingPotionConfig HealingPotion;
    }
}