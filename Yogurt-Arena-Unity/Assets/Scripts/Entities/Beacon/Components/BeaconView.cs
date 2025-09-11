namespace Yogurt.Arena
{
    public class BeaconView : MonoBehaviour, IComponent, IDisposable
    {
        public Transform Transform;

        public void Dispose()
        {
            GetComponent<PoolLink>().Release();
        }
    }
}