using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateMoveInputJob
    {
        public void Run(InputFieldAspect inputField)
        {
            inputField.Run(Update);
            
            
            void Update()
            {
                Time time = Query.Single<Time>();
                
                InputConfig config = inputField.Config;
                MoveInputReader reader = inputField.MoveInputReader;
                InputState inputState = inputField.Input;

                Vector2 delta = Vector2.zero;
                if (reader.IsDown)
                {
                    delta = -reader.Delta;
                    delta *= config.Sensitivity;
                    inputState.CumulativeVelocity = Vector2.Lerp(inputState.CumulativeVelocity, delta, config.AccumulativeValue * time);
                }
                else
                {
                    delta = inputState.CumulativeVelocity;
                    inputState.CumulativeVelocity = Vector2.Lerp(inputState.CumulativeVelocity, Vector2.zero, config.DeAccumulativeValue * time);
                }

                inputState.MoveDelta = AddCameraRotation(delta);
                reader.Delta = Vector2.zero;
            }
        }
        
        private static Vector2 AddCameraRotation(Vector2 delta)
        {
            Transform cameraTransform = Query.Single<CameraAspect>().View.transform;

            float cameraRotation = cameraTransform.eulerAngles.y;
            return delta.Rotate(-cameraRotation);
        }
    }
}