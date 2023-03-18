using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class Assets : IComponent
    {
        public const string PREFABS_PATH = "Prefabs/";
        public const string LEVELS_PATH = "Levels/";
        
        public readonly SO<Data> Data = new("Data"); 
        
        public readonly Asset<World> World = new Asset<World>(PREFABS_PATH + "World");
        public readonly Asset<CameraView> Camera = new Asset<CameraView>(PREFABS_PATH + "Camera");
        public readonly Asset<InputFieldView> InputField = new Asset<InputFieldView>(PREFABS_PATH + "InputField");
        public readonly Asset<BeaconView> Beacon = new Asset<BeaconView>(PREFABS_PATH + "Beacon");
        public readonly Asset<AgentView> Player = new Asset<AgentView>(PREFABS_PATH + "Player");
        public readonly Asset<AgentView> Enemy_1 = new Asset<AgentView>(PREFABS_PATH + "Enemy_1");
        
        public readonly Asset<Level> Level = new Asset<Level>(LEVELS_PATH + "Level");
    }
}