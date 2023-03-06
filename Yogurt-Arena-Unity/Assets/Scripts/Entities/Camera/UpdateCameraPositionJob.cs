using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateCameraPositionJob
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
            InputFieldAspect inputField = Query.Single<InputFieldAspect>();
            CameraAspect camera = Query.Single<CameraAspect>();

            camera.Transform.position += new Vector3(inputField.Input.MoveDelta.x, 0, inputField.Input.MoveDelta.y);
        }
    }
}