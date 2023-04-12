using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraFollowJob : IUpdateJob
    {
        public void Update()
        {
            float dt = Time.deltaTime * 100;
            
            CameraAspect camera = Query.Single<CameraAspect>();
            
            Vector3 currentPos = camera.View.transform.position;
            Vector3 followPoint = GetFollowPoint();
		
            Vector3 lerpPoint = new Vector3(
                Mathf.Lerp(currentPos.x, followPoint.x, camera.Data.SmoothValue * dt), 
                Mathf.Lerp(currentPos.y, followPoint.y, camera.Data.SmoothValue / 5 * dt), 
                Mathf.Lerp(currentPos.z, followPoint.z, camera.Data.SmoothValue * dt));
		
            camera.View.transform.position = lerpPoint;
        }

        private static Vector3 GetFollowPoint()
        {
            BeaconAspect beacon = Query.Single<BeaconAspect>();
            return beacon.Body.Destination;
        }
    }
}