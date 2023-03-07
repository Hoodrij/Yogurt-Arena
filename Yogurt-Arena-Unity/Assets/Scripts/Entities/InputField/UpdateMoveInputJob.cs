using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateMoveInputJob : IUpdateJob
    {
        public void Update()
        {
            const float smoothValue = 0.1f;
            const float sensitivity = 0.05f;

            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            MoveInputReader reader = inputField.MoveInputReader;
            InputData inputData = inputField.Input;

            Vector2 delta = Vector2.zero;
            if (reader.IsDown)
            {
                delta = -reader.Delta;
                delta *= sensitivity;
                inputData.CumulativeVelocity = Vector2.Lerp(inputData.CumulativeVelocity, delta, smoothValue);
            }
            else
            {
                delta = inputData.CumulativeVelocity;
                inputData.CumulativeVelocity = Vector2.Lerp(inputData.CumulativeVelocity, Vector2.zero, smoothValue);
            }

            inputData.MoveDelta = AddCameraRotation(delta);
            reader.Delta = Vector2.zero;
        }
        
        private Vector2 AddCameraRotation(Vector2 delta)
        {
            Transform cameraTransform = Query.Single<CameraAspect>().Transform;

            float cameraRotation = cameraTransform.eulerAngles.y;
            return delta.Rotate(-cameraRotation);
        }
    }
}