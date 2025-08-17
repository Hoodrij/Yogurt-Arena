namespace Yogurt.Arena
{
    public struct ChargeUpdateBulletPositionJob
    {
        public async void Run(BulletAspect bullet)
        {
            AgentAspect owner = bullet.Owner.Value;
            bullet.Run(Update);
            return;


            async void Update()
            {
                if (!owner.Exist())
                    return;
                
                Transform transform = bullet.View.transform;
                Vector3 position = owner.Body.MiddlePoint;
                bullet.Body.Position = transform.position = position;
            }
        }
    }
}