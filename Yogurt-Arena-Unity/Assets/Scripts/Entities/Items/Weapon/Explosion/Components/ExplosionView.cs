namespace Yogurt.Arena
{
    public class ExplosionView : MonoBehaviour, IDisposable
    {
        public Transform View;

        public void Dispose()
        {
            GetComponent<PoolLink>().Release();
        }
    }
}