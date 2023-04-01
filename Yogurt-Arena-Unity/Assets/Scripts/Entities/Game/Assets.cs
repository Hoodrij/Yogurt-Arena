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
        
        public readonly IAsset<World> World = new Asset<World>(PREFABS_PATH + "World");
        public readonly IAsset<CameraView> Camera = new Asset<CameraView>(PREFABS_PATH + "Camera");
        public readonly IAsset<InputFieldView> InputField = new Asset<InputFieldView>(PREFABS_PATH + "InputField");
        public readonly IAsset<BeaconView> Beacon = new Asset<BeaconView>(PREFABS_PATH + "Beacon");
        
        public readonly IAsset<AgentView> Player = new Asset<AgentView>(AGENTS_PATH + "Player");
        public readonly IAsset<AgentView> Enemy_1 = new Asset<AgentView>(AGENTS_PATH + "Enemy_1");

        public readonly IAsset<BulletView> RifleBullet = new PooledAsset<BulletView>(WEAPONS_PATH + "RifleBullet");

        public readonly IAsset<Level> Level = new Asset<Level>(LEVELS_PATH + "Level");
    }
}