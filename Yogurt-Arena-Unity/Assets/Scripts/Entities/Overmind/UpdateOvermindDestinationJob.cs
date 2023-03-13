namespace Yogurt.Arena
{
    public struct UpdateOvermindDestinationJob : IUpdateJob
    {
        public void Update()
        {
            AgentAspect player = Query.Of<AgentAspect>().With<PlayerTag>().Single();

            foreach (AgentAspect agentAspect in Query.Of<AgentAspect>().Without<PlayerTag>())
            {
                agentAspect.State.Destination = player.State.Position;
            }
        }
    }
}