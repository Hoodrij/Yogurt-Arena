using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct AgentMoveJob : IUpdateJob
    {
	    private Data data => Query.Single<Data>();
	    
        public void Update()
        {
	        float dt = Time.deltaTime;
            
            foreach (AgentAspect agent in Query.Of<AgentAspect>())
            {
		        UpdateState(agent.Body, dt);

		        agent.View.transform.position = agent.Body.Position;
            }
        }

        private void UpdateState(BodyState state, float dt)
		{
			NavMesh.SamplePosition(state.Destination, out var destinationHit, 100, NavMesh.AllAreas);
			Vector3 requiredPos = destinationHit.position;
			Vector3 currentPos = state.Position;

			NavMeshPath path = CalculatePath(currentPos, requiredPos);
			Vector3 requiredVelocity = GetNextVelocityByPath(data.Agent.MoveSpeed * dt, path);
			float distanceToTarget = (currentPos - requiredPos).magnitude;
			requiredVelocity = GetSmoothedVelocity(distanceToTarget, requiredVelocity, state.Velocity, dt);

			Vector3 newPos = currentPos + requiredVelocity;
			NavMesh.SamplePosition(newPos, out var hit, 1, NavMesh.AllAreas);
			
			state.Position = hit.position;
			state.Velocity = requiredVelocity;
		}

        private static NavMeshPath CalculatePath(Vector3 startPos, Vector3 endPos)
		{
			NavMeshPath path = new NavMeshPath();
			NavMesh.CalculatePath(startPos, endPos, NavMesh.AllAreas, path);
			return path;
		}

		private static Vector3 GetNextVelocityByPath(float speed, NavMeshPath path)
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
		
		private Vector3 GetSmoothedVelocity(float distanceToTarget, Vector3 requiredVelocity, Vector3 prevVelocity, float dt)
		{
			Vector3 velocity;
			if (distanceToTarget < prevVelocity.magnitude * data.Agent.MoveSpeed * 0.5f)
			{
				velocity = requiredVelocity * (distanceToTarget * data.Agent.MoveSmoothValue * 2);
			}
			else
			{
				velocity = Vector3.Lerp(prevVelocity, requiredVelocity, data.Agent.MoveSmoothValue);
			}

			velocity = velocity.ClampMagnitude(data.Agent.MoveSpeed * dt);
			
			return velocity;
		}
    }
}