using UnityEngine;

namespace Yogurt.Arena
{
    public struct GetFollowPointJob
    {
        public Vector3 Run(CameraAspect camera)
        {
            BeaconAspect beacon = Query.Single<BeaconAspect>();
            Vector3 beaconPoint = beacon.Body.RawDestination;

            // Do not follow mouse when player is dead (or not present)
            PlayerAspect player = Query.Single<PlayerAspect>();
            if (!player.Exist())
                return beaconPoint;

            float mouseInfluence = Mathf.Clamp01(camera.Config.MouseInfluence);

            Ray ray = camera.Camera.ScreenPointToRay(Input.mousePosition);

            Vector3 mouseWorld = beaconPoint;
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
            {
                mouseWorld = hit.point;
            }
            else
            {
                Plane plane = new Plane(Vector3.up, new Vector3(0f, beaconPoint.y, 0f));
                if (plane.Raycast(ray, out float dist))
                {
                    mouseWorld = ray.GetPoint(dist);
                }
                else
                {
                    return beaconPoint;
                }
            }

            mouseWorld.y = beaconPoint.y;
            Vector3 offset = mouseWorld - beaconPoint;

            float maxDist = camera.Config.MouseInfluenceMaxDistance;
            if (maxDist > 0f)
            {
                float len = offset.magnitude;
                if (len > maxDist)
                    offset *= (maxDist / len);
            }

            return beaconPoint + offset * mouseInfluence;
        }
    }
}
