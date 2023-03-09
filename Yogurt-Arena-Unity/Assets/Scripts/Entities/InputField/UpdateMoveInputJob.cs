﻿using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateMoveInputJob : IUpdateJob
    {
        public void Update()
        {
            InputData data = Query.Single<Data>().Input;
            
            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            MoveInputReader reader = inputField.MoveInputReader;
            InputState inputState = inputField.Input;

            Vector2 delta = Vector2.zero;
            if (reader.IsDown)
            {
                delta = -reader.Delta;
                delta *= data.Sensitivity;
                inputState.CumulativeVelocity = Vector2.Lerp(inputState.CumulativeVelocity, delta, data.SmoothValue);
            }
            else
            {
                delta = inputState.CumulativeVelocity;
                inputState.CumulativeVelocity = Vector2.Lerp(inputState.CumulativeVelocity, Vector2.zero, data.SmoothValue);
            }

            inputState.MoveDelta = AddCameraRotation(delta);
            reader.Delta = Vector2.zero;
        }
        
        private static Vector2 AddCameraRotation(Vector2 delta)
        {
            Transform cameraTransform = Query.Single<CameraAspect>().Transform;

            float cameraRotation = cameraTransform.eulerAngles.y;
            return delta.Rotate(-cameraRotation);
        }
    }
}