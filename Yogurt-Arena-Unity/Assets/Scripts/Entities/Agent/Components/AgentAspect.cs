namespace Yogurt.Arena;

public struct AgentAspect : IAspect
{
    public Entity Entity { get; set; }

    public AgentConfig Config => this.Get<AgentConfig>();

    public AgentId Id => this.Get<AgentId>();
    public BodyState Body => this.Get<BodyState>();
    public Health Health => this.Get<Health>();
    public AgentView View => this.Get<AgentView>();
    public BattleState BattleState => this.Get<BattleState>();
    public Inventory Inventory => this.Get<Inventory>();
}