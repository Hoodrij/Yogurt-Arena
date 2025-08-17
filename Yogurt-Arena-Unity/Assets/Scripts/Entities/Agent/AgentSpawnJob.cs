namespace Yogurt.Arena
{
    public struct AgentSpawnJob
    {
        public async UniTask<AgentAspect> Run(AgentConfig config, Vector3 position)
        {
            NavMesh.SamplePosition(position, out var hit, 100, NavMesh.AllAreas);
            position = hit.position;
            
            AgentAspect agent = await new AgentFactoryJob().Run(config, config.Team);
            agent.Add(new Kinematic());
            agent.Body.Position = position;
            agent.Body.Destination = position;
            
            new AgentMoveJob().Run(agent);
            new AgentLookJob().Run(agent);

            await new GiveItemJob().Run(agent.Config.Weapon, agent);
            
            await ActivateAgent();
            return agent;


            async UniTask ActivateAgent()
            {
                await RunAnimation();
                agent.Remove<Kinematic>();
            }

            async UniTask RunAnimation()
            {
                float animationTime = 0.2f;
                float activationTime = animationTime * 0.9f;
                agent.View.transform.DOKill();
                agent.View.transform.localScale = new Vector3(0, 2, 0);
                DOTween.Sequence()
                    .Append(agent.View.transform.DOScale(new Vector3(1.3f, 0.4f, 1.3f), animationTime))
                    .Append(agent.View.transform.DOScale(Vector3.one, animationTime * 4).SetEase(Ease.OutElastic))
                    ;

                await Wait.Seconds(activationTime, agent.Life());
            }
        }
    }
}