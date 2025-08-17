namespace Yogurt.Arena
{
    public struct AgentDeathJob : IDeathJob
    {
        public async UniTaskVoid Run(Entity entity)
        {
            AgentConfig config = entity.Get<AgentConfig>();
            Vector3 position = entity.Get<BodyState>().MiddlePoint;

            AgentDeathVFX vfx = await config.DeathVFX.Spawn();
            vfx.transform.position = position;

            await Wait.Seconds(0.5f);
            vfx.GetComponent<PoolLink>().Release();
        }
    }
}