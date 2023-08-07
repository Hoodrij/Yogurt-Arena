using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Components;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Config : ScriptableObject, IComponent
    {
        [SerializeReference]
        public List<ScriptableObject> All;
        
        public Asset<World> World;
        public Asset<LevelPartTag>[] Levels;

        public UIConfig UI;
        public InputConfig Input;
        public CameraConfig Camera;
        public BeaconConfig Beacon;
        public OvermindConfig Overmind;
        
        [Header("Agents")]
        public AgentConfig Player;
        public AgentConfig ChargeEnemy;
        public AgentConfig BombEnemy;

        [Header("Items")]
        public ItemSpotConfig ItemSpot;
    }
}