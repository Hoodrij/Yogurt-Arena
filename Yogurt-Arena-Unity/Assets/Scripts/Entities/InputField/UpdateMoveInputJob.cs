using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateMoveInputJob
    {
        public async void Run()
        {
            while (true)
            {
                await UniTask.Yield();
                Update();
            }
        }

        private void Update()
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

            inputData.MoveDelta = delta;
            reader.Delta = Vector2.zero;
        }
    }
}