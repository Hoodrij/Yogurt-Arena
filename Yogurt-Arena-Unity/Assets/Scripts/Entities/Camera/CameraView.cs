using UnityEngine;

namespace Yogurt.Arena
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] public UnityEngine.Camera Camera;
        
        private void Awake()
        {
            new CameraFactoryJob().Run(this);
        }
    }
}