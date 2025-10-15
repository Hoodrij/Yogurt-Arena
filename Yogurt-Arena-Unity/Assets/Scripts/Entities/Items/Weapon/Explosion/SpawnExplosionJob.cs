namespace Yogurt.Arena;

public struct SpawnExplosionJob
{
    public async UniTask Run(ExplosionConfig config, Vector3 position)
    {
        using ExplosionView vfx = await config.Asset.Spawn();
        Transform view = vfx.View;
        float duration = 0.15f;
        float halfDuration = 0.07f;
            
        vfx.transform.position = position;
        view.localScale = Vector3.zero;
        await view.DOScale(config.Damage.Radius * 2, halfDuration);
        await view.DOScale(0, duration).SetEase(Ease.OutSine);
        await Wait.Seconds(0.5f);
    }
}