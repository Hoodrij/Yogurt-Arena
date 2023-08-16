using System.Collections.Generic;
using UnityEngine;
using Yogurt.Arena.Tools;

namespace Yogurt.Arena
{
    [CreateAssetMenu]
    public class Config : ScriptableObject, IComponent
    {
        [SerializeReference]
        public List<ScriptableObject> All;
        
        public Asset<World> World;
        public Asset<LocationPartTag>[] Locations;

        public UIConfig UI;
        public InputConfig Input;
        public CameraConfig Camera;
        public BeaconConfig Beacon;
        public OvermindConfig Overmind;

        [Header("Items")]
        public ItemSpotConfig ItemSpot;
    }
}