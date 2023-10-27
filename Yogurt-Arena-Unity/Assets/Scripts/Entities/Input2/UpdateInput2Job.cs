using UnityEngine;

namespace Yogurt.Arena
{
    public struct UpdateInput2Job
    {
        public void Run(InputAspect inputAspect)
        {
            inputAspect.Run(Update);
            return;


            void Update()
            {
                float sensitivity = Query.Single<InputConfig>().Sensitivity;
                
                float horizontal = Input.GetAxisRaw("Horizontal");
                float vertical = Input.GetAxisRaw("Vertical");
                Vector2 input = new Vector2(horizontal, vertical) * sensitivity;
                input = new AddCameraRotationJob().Run(input);

                inputAspect.State.MoveDelta = input;
            }
        }
    }
}