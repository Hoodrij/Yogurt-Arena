namespace Yogurt.Arena
{
    public struct AgentMoveJob
    {
	    public void Run(AgentAspect agent)
	    {
		    Time time = Query.Single<Time>();
		    
		    agent.Run(Update);
		    return;


		    void Update()
		    {
			    if (!agent.Has<Kinematic>())
			    {
					UpdateState();
			    }

			    agent.View.transform.position = agent.Body.Position;
	        }
		    
		    void UpdateState()
	        {
		        BodyState body = agent.Body;

		        if (!NavMesh.SamplePosition(body.Destination, out var destinationHit, 100, NavMesh.AllAreas))
			        return;
				Vector3 requiredPos = destinationHit.position;
				Vector3 currentPos = body.Position;

				NavMeshPath path = CalculatePath(currentPos, requiredPos);
				Vector3 requiredVelocity = GetNextVelocityByPath(agent.Config.MoveSpeed * time, path);
				float distanceToTarget = (currentPos - requiredPos).magnitude;
				requiredVelocity = GetSmoothedVelocity(distanceToTarget, requiredVelocity, time);

				Vector3 newPos = currentPos + requiredVelocity;
				NavMesh.SamplePosition(newPos, out var hit, 1, NavMesh.AllAreas);
				
				body.Position = hit.position;
				body.Velocity = requiredVelocity;
			}

	        static NavMeshPath CalculatePath(Vector3 startPos, Vector3 endPos)
			{
				NavMeshPath path = new NavMeshPath();
				NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, path);
				return path;
			}
	        
	        static Vector3 GetNextVelocityByPath(float speed, NavMeshPath path)
			{
				if (path.status != NavMeshPathStatus.PathComplete) 
					return Vector3.zero;
				
			    float soFar = 0.0f;
			    Vector3 finalPoint = path.corners.Last();

			    for (int i = 0; i < path.corners.Length - 1; i++)
		        {
		            float segmentDistance = (path.corners[i + 1] - path.corners[i]).magnitude;
		            if (soFar + segmentDistance <= speed)
		            {
		                soFar += segmentDistance;
		            }
		            else
		            {
		                finalPoint = path.corners[i] + (path.corners[i + 1] - path.corners[i]).normalized * (speed - soFar);
		                break;
		            }
		        }
		        return finalPoint - path.corners.First();
		    }
	        
	        Vector3 GetSmoothedVelocity(float distanceToTarget, Vector3 requiredVelocity, Time time)
			{
				AgentConfig config = agent.Config;
				Vector3 prevVelocity = agent.Body.Velocity;

				Vector3 velocity;
				if (distanceToTarget < prevVelocity.magnitude * config.SlowDistance)
				{
					velocity = requiredVelocity * (distanceToTarget * config.MoveSmoothValue * 2);
				}
				else
				{
					velocity = Vector3.Lerp(prevVelocity, requiredVelocity, config.MoveSmoothValue);
				}

				velocity = velocity.ClampMagnitude(config.MoveSpeed * time);
				
				return velocity;
			}
	    }
    }
}