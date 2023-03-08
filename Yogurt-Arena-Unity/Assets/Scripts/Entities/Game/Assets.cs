﻿using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class Assets : IComponent
    {
        public const string PREFABS_PATH = "Prefabs/";
        
        public readonly SO<Data> Data = new("Data"); 
        
        public readonly Asset<World> World = new Asset<World>(PREFABS_PATH + "World");
        public readonly Asset<CameraView> Camera = new Asset<CameraView>(PREFABS_PATH + "Camera");
        public readonly Asset<InputFieldView> InputField = new Asset<InputFieldView>(PREFABS_PATH + "InputField");
        public readonly Asset<BeaconView> Beacon = new Asset<BeaconView>(PREFABS_PATH + "Beacon");
        public readonly Asset<AgentView> Agent = new Asset<AgentView>(PREFABS_PATH + "Agent");
    }
}