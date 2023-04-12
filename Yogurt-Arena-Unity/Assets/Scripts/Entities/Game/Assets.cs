using System;
using Yogurt.Roguelike.Tools;

namespace Yogurt.Arena
{
    [Serializable]
    public class Assets : IComponent
    {
        public Asset<World> World;
        public Asset<Level> Level;
        public Asset<CameraView> Camera;
        public Asset<InputFieldView> InputField;
        public Asset<BeaconView> Beacon;

        public Asset<AgentView> Player;
        public PooledAsset<AgentView> Enemy_1;

        public PooledAsset<BulletView> RifleBullet;
    }
}