using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Yogurt.Arena
{
    public struct AgentMoveJob : IUpdateJob
    {
	    public void Update()
        {
	        float dt = Time.deltaTime;
            
            foreach (AgentAspect agent in Query.Of<AgentAspect>())
            {
		        UpdateState(agent, dt);

		        agent.View.transform.position = agent.Body.Position;
            }
        }

        private void UpdateState(AgentAspect agent, float dt)
        {
	        BodyState body = agent.Body;

	        NavMesh.SamplePosition(body.Destination, out var destinationHit, 100, NavMesh.AllAreas);
			Vector3 requiredPos = destinationHit.position;
			Vector3 currentPos = body.Position;

			NavMeshPath path = CalculatePath(currentPos, requiredPos);
			Vector3 requiredVelocity = GetNextVelocityByPath(agent.Data.MoveSpeed * dt, path);
			float distanceToTarget = (currentPos - requiredPos).magnitude;
			requiredVelocity = GetSmoothedVelocity(agent, distanceToTarget, requiredVelocity, dt);

			Vector3 newPos = currentPos + requiredVelocity;
			NavMesh.SamplePosition(newPos, out var hit, 1, NavMesh.AllAreas);
			
			body.Position = hit.position;
			body.Velocity = requiredVelocity;
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
		
		private Vector3 GetSmoothedVelocity(AgentAspect agent, float distanceToTarget, Vector3 requiredVelocity, float dt)
		{
			AgentData data = agent.Data;
			Vector3 prevVelocity = agent.Body.Velocity;

			Vector3 velocity;
			if (distanceToTarget < prevVelocity.magnitude * data.MoveSpeed * 0.5f)
			{
				velocity = requiredVelocity * (distanceToTarget * data.MoveSmoothValue * 2);
			}
			else
			{
				velocity = Vector3.Lerp(prevVelocity, requiredVelocity, data.MoveSmoothValue);
			}

			velocity = velocity.ClampMagnitude(data.MoveSpeed * dt);
			
			return velocity;
		}
    }
}