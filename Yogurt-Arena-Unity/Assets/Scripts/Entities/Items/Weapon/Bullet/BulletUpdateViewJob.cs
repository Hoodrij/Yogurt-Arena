namespace Yogurt.Arena
{
    public struct BulletUpdateViewJob : IUpdateJob
    {
        public void Update()
        {
            foreach (BulletAspect bullet in Query.Of<BulletAspect>())
            {
                bullet.View.transform.position = bullet.Body.Position;
            }
        }
    }
}