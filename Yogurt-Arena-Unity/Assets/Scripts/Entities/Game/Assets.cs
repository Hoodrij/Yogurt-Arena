using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    public class Assets : IComponent
    {
        private const string PREFABS_PATH = "Prefabs/";
        private const string LEVELS_PATH = "Levels/";
        private const string WEAPONS_PATH = PREFABS_PATH + "Weapons/";
        private const string AGENTS_PATH = PREFABS_PATH + "Agents/";
        
        public readonly SO<Data> Data = new("Data"); 
        
        public readonly Asset<World> World = new Asset<World>(PREFABS_PATH + "World");
        public readonly Asset<CameraView> Camera = new Asset<CameraView>(PREFABS_PATH + "Camera");
        public readonly Asset<InputFieldView> InputField = new Asset<InputFieldView>(PREFABS_PATH + "InputField");
        public readonly Asset<BeaconView> Beacon = new Asset<BeaconView>(PREFABS_PATH + "Beacon");
        
        public readonly Asset<AgentView> Player = new Asset<AgentView>(AGENTS_PATH + "Player");
        public readonly Asset<AgentView> Enemy_1 = new Asset<AgentView>(AGENTS_PATH + "Enemy_1");

        public readonly Asset<BulletView> RifleBullet = new Asset<BulletView>(WEAPONS_PATH + "RifleBullet");

        public readonly Asset<Level> Level = new Asset<Level>(LEVELS_PATH + "Level");
    }
}