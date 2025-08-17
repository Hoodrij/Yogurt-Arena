namespace Yogurt.Arena;

public struct UpdateMoveInputJob
{
    public void Run(InputFieldAspect inputField)
    {
        inputField.Run(Update);
        return;

        void Update()
        {
            MoveInputReader reader = inputField.MoveInputReader;
            InputState inputState = inputField.Input;

            if (reader.HasClick)
            {
                reader.HasClick = false;
                CameraAspect cameraAspect = Query.Single<CameraAspect>();
                if (cameraAspect.Exist())
                {
                    Camera cam = cameraAspect.Camera;
                    Ray ray = cam.ScreenPointToRay(reader.ClickScreenPosition);
                    Vector3 worldPoint;
                    if (Physics.Raycast(ray, out RaycastHit hit, 1000f, Physics.DefaultRaycastLayers, QueryTriggerInteraction.Ignore))
                    {
                        worldPoint = hit.point;
                    }
                    else
                    {
                        Plane ground = new Plane(Vector3.up, Vector3.zero);
                        if (ground.Raycast(ray, out float dist))
                        {
                            worldPoint = ray.GetPoint(dist);
                        }
                        else
                        {
                            return;
                        }
                    }

                    inputState.HasClick = true;
                    inputState.ClickWorldPosition = worldPoint;
                }
            }
        }
    }
}