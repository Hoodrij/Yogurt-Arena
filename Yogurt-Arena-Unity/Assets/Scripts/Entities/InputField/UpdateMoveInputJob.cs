using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateMoveInputJob
    {
        public void Run(InputFieldAspect inputField)
        {
            inputField.Run(Update);
            return;


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

                inputState.MoveDelta = new AddCameraRotationJob().Run(delta);
                reader.Delta = Vector2.zero;
            }
        }
    }
}