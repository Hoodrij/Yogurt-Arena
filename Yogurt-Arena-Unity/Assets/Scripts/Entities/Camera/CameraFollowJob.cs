namespace Yogurt.Arena
{
    public struct CameraFollowJob
    {
        public void Run(CameraAspect camera)
        {
            float velX = 0f, velY = 0f, velZ = 0f;
            camera.Run(Update);
            return;
            
            void Update()
            {
                Vector3 currentPos = camera.View.transform.position;
                Vector3 followPoint = new GetFollowPointJob().Run(camera);
		    
                Time time = Query.Single<Time>();
                float smoothValue = camera.Config.SmoothValue;
                float smoothTimeBased = time.ExpectedDelta / smoothValue;
                float smoothTimeY = smoothTimeBased * 5f;

                float dt = UnityEngine.Time.deltaTime;
                float newX = Mathf.SmoothDamp(currentPos.x, followPoint.x, ref velX, smoothTimeBased, Mathf.Infinity, dt);
                float newY = Mathf.SmoothDamp(currentPos.y, followPoint.y, ref velY, smoothTimeY, Mathf.Infinity, dt);
                float newZ = Mathf.SmoothDamp(currentPos.z, followPoint.z, ref velZ, smoothTimeBased, Mathf.Infinity, dt);

                camera.View.transform.position = new Vector3(newX, newY, newZ);
            }
        }
    }
}