namespace Yogurt.Arena
{
    public struct AgentAspect : IAspect
    {
        public Entity Entity { get; set; }

        public AgentId Id => this.Get<AgentId>();
        public BodyState Body => this.Get<BodyState>();
        public AgentView View => this.Get<AgentView>();
        public AgentBattleState BattleState => this.Get<AgentBattleState>();
        public ItemsCollection Items => this.Get<ItemsCollection>();
    }
}