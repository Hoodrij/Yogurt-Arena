namespace Yogurt.Arena;

public struct SpawnBeaconVfxJob
{
    public async UniTask Run(BeaconConfig config, Vector3 position)
    {
        using BeaconView view = await config.Asset.Spawn();
        Transform t = view.transform;
        t.position = position;
        t.localScale = Vector3.zero;

        await t.DOScale(Vector3.one, config.AppearDuration / 5);
        await t.DOPunchScale(Vector3.one * 2, config.AppearDuration);
        await t.DOScale(Vector3.zero, config.DisappearDuration);
    }
}

