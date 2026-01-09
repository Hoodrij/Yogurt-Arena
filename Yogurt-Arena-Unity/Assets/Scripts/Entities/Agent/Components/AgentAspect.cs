namespace Yogurt.Arena;

public record struct AgentAspect(Entity Entity) : IAspect
{
    public AgentConfig Config => this.Get<AgentConfig>();

    public ref AgentId Id => ref this.Get<AgentId>();
    public ref BodyState Body => ref this.Get<BodyState>();
    public ref Health Health => ref this.Get<Health>();
    public ref AgentView View => ref this.Get<AgentView>();
    public ref BattleState BattleState => ref this.Get<BattleState>();
    public ref Inventory Inventory => ref this.Get<Inventory>();
}