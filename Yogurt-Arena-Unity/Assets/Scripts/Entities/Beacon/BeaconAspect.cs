namespace Yogurt.Arena
{
    public struct BeaconAspect : IAspect
    {
        public Entity Entity { get; set; }

        public BeaconBodyState Body => this.Get<BeaconBodyState>();
        public BeaconView View => this.Get<BeaconView>();
    }
}