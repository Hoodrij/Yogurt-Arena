namespace Yogurt.Arena.Tools
{
    [Serializable]
    public class PooledAsset<TComponent> : IAsset<TComponent> where TComponent : Component
    {
        public Asset<TComponent> asset;
        private Pool pool;

        public async UniTask<TComponent> Spawn()
        {
            pool ??= new Pool(Factory);
            GameObject gameObject = await pool.Pop();
            return gameObject.GetComponent<TComponent>();

            async UniTask<GameObject> Factory() => (await asset.Spawn()).gameObject;
        }
    }
}