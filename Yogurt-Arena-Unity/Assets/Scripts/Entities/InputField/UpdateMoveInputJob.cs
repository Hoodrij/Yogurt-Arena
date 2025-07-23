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
                InputConfig config = inputField.Config;
                MoveInputReader reader = inputField.MoveInputReader;

                if (!reader.ScreenPosition.HasValue) 
                    return;

                inputField.Input.Position = InputToWorldPosition(reader.ScreenPosition.Value, config); 
                reader.ScreenPosition = null;
            }
            
            Vector3 InputToWorldPosition(Vector3 input, InputConfig config)
            {
                CameraAspect cameraAspect = Query.Single<CameraAspect>();

                Ray ray = cameraAspect.Camera.ScreenPointToRay(input);
                if (Physics.Raycast(ray, out RaycastHit hit, config.LayerMask))
                {
                    return hit.point;
                }
                else
                {
                    new Plane(Vector3.up, Vector3.zero)
                        .Raycast(ray, out float distance);
                    
                    return ray.GetPoint(distance);
                }
            }
        }
    }
}