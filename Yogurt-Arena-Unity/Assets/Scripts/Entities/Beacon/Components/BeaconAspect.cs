namespace Yogurt.Arena
{
    public struct BeaconAspect : IAspect
    {
        public Entity Entity { get; set; }

        public BeaconConfig Config => this.Get<BeaconConfig>();

        public BeaconBodyState Body => this.Get<BeaconBodyState>();
        public BeaconView View => this.Get<BeaconView>();
    }
}