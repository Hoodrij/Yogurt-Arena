using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct AgentSpawnJob
    {
        public async UniTask<AgentAspect> Run(AgentConfig config, Vector3 position)
        {
            NavMesh.SamplePosition(position, out var hit, 100, NavMesh.AllAreas);
            position = hit.position;
            
            AgentAspect agent = await new AgentFactoryJob().Run(config, config.Team);
            agent.Add<Kinematic>();
            agent.Body.Position = position;
            agent.Body.Destination = position;
            agent.View.transform.position = agent.Body.Position;
            
            ActivateAgent();
            return agent;


            async void ActivateAgent()
            {
                await RunAnimation();
                agent.Remove<Kinematic>();
            }

            async UniTask RunAnimation()
            {
                float animationTime = 0.2f;
                float activationTime = animationTime * 0.9f;
                agent.View.DOKill();
                agent.View.transform.localScale = new Vector3(0, 2, 0);
                // agent.View.transform.DOShakeScale(animationTime, 2, 7, 0);
                DOTween.Sequence()
                    .Append(agent.View.transform.DOScale(new Vector3(1.3f, 0.4f, 1.3f), animationTime))
                    .Append(agent.View.transform.DOScale(Vector3.one, animationTime * 4).SetEase(Ease.OutElastic))
                    ;

                await Wait.Seconds(activationTime);
            }
        }
    }
}