using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraFollowJob : IUpdateJob
    {
        public void Update()
        {
            float dt = Time.deltaTime * 100;
            
            Data data = Query.Single<Data>();
            CameraAspect camera = Query.Single<CameraAspect>();
            
            Vector3 currentPos = camera.Transform.position;
            Vector3 followPoint = GetFollowPoint();
		
            Vector3 lerpPoint = new Vector3(
                Mathf.Lerp(currentPos.x, followPoint.x, data.Camera.SmoothValue * dt), 
                Mathf.Lerp(currentPos.y, followPoint.y, data.Camera.SmoothValue / 5 * dt), 
                Mathf.Lerp(currentPos.z, followPoint.z, data.Camera.SmoothValue * dt));
		
            camera.Transform.position = lerpPoint;
        }

        private static Vector3 GetFollowPoint()
        {
            BeaconAspect beacon = Query.Single<BeaconAspect>();
            return beacon.State.Destination;
        }
    }
}