namespace Yogurt.Arena;

public struct EnemyDeathJob : IDeathJob
{
    public async UniTaskVoid Run(Entity entity)
    {
        Vector3 position = entity.Get<BodyState>().MiddlePoint;

        new AgentDeathJob().Run(entity);
        await new SpawnItemDropJob().Run(position);
    }
}
