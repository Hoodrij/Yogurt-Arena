using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateCameraPositionJob : IUpdateJob
    {
        public void Update()
        {
            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            CameraAspect camera = Query.Single<CameraAspect>();

            camera.Transform.position += new Vector3(inputField.Input.MoveDelta.x, 0, inputField.Input.MoveDelta.y);
        }
    }
}