namespace Yogurt.Arena;

public struct AgentLookJob
{
    private const float MIN_LOOK_MAGNITUDE = 0.001f;

    public void Run(AgentAspect agent)
    {
        Time time = Query.Single<Time>();
        int frameRate = time.TARGET_FRAME_RATE;
            
        agent.Run(Update);
        return;


        void Update()
        {
            if (!agent.Has<Kinematic>())
            {
                UpdateState();
            }
                
            agent.View.transform.forward = agent.Body.Forward;
        }

        void UpdateState()
        {
            BodyState body = agent.Body;

            if (agent.BattleState.Target.Exist())
            {
                BodyState targetBody = agent.BattleState.Target.Body;
                body.LookPoint = targetBody.Position.WithY(body.Position.y);
            }
            else
            {
                body.LookPoint = body.Position + body.Velocity.WithY(0) * frameRate;
            }

            Vector3 lookVector = body.LookPoint - body.Position;
            if (lookVector.sqrMagnitude > MIN_LOOK_MAGNITUDE)
            {
                body.Forward = Vector3.RotateTowards(body.Forward, lookVector.normalized, agent.Config.LookSmoothValue * time, agent.Config.LookSmoothValue * time);
            }
        }
    }
        
}