using UnityEngine;

namespace Yogurt.Arena
{
    public struct CameraFollowJob
    {
        public void Run(CameraAspect camera)
        {
            camera.Run(Update);
            return;


            void Update()
            {
                Time time = Query.Single<Time>();
                
                Vector3 currentPos = camera.View.transform.position;
                Vector3 followPoint = GetFollowPoint();
		    
                Vector3 lerpPoint = new Vector3(
                    Mathf.Lerp(currentPos.x, followPoint.x, camera.Config.SmoothValue * time), 
                    Mathf.Lerp(currentPos.y, followPoint.y, camera.Config.SmoothValue / 5 * time), 
                    Mathf.Lerp(currentPos.z, followPoint.z, camera.Config.SmoothValue * time));
		    
                camera.View.transform.position = lerpPoint;
            }

            Vector3 GetFollowPoint()
            {
                BeaconAspect beacon = Query.Single<BeaconAspect>();
                return beacon.Body.RawDestination;
            }
        }
    }
}