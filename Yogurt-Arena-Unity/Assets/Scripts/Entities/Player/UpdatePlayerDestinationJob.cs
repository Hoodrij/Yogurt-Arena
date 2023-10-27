using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdatePlayerDestinationJob
    {
        public void Run(PlayerAspect player)
        {
            player.Run(Update);
            return;


            void Update()
            {
                InputAspect input = Query.Single<InputAspect>();
                Vector3 moveDir = input.State.MoveDelta.ToV3XZ();

                player.Agent.Body.Destination = player.Agent.Body.Position + moveDir;
            }
        }
    }
}