using UnityEngine;

namespace Yogurt.Arena
{
    public struct AddCameraRotationJob
    {
        public Vector2 Run(Vector2 delta)
        {
            CameraAspect cameraAspect = Query.Single<CameraAspect>();
            if (!cameraAspect.Exist())
            {
                return delta;
            }
            Transform cameraTransform = cameraAspect.View.transform;

            float cameraRotation = cameraTransform.eulerAngles.y;
            return delta.Rotate(-cameraRotation);
        }
    }
}