namespace Yogurt.Arena;

public struct KillBulletJob
{
    public async UniTask Run(BulletAspect bullet)
    {
        bullet.Add(new Kinematic());
        bullet.Body.Velocity = Vector3.zero;

        float t = 0.1f;

        if (bullet.View.Particle != null)
        {
            bullet.View.Particle.Play();
            _ = bullet.View.transform.DOScale(0, t);
            await Wait.Seconds(0.3f, bullet.Life());
        }
        else
        {
            _ = bullet.View.transform.DOScale(2, t);
            await Wait.Seconds(t, bullet.Life());
            _ = bullet.View.transform.DOScale(0, t);
            await Wait.Seconds(t, bullet.Life());
        }

        bullet.Kill();
    }
}