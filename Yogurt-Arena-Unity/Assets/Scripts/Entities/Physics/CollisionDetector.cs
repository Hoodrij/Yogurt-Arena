using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena
{
    public class CollisionDetector : MonoBehaviour, IComponent
    {
        public LayerMask Mask;
        private bool hasCollision;
        private GameObject lastCollision;
        
        public async UniTask<GameObject> WaitForCollision()
        {
            await UniTask.WaitWhile(() => !hasCollision);
            return lastCollision;
            
        }

        public static bool IsInLayerMask(int layer, LayerMask layermask)
        {
            return layermask == (layermask | (1 << layer));
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (IsInLayerMask(gameObject.layer, Mask))
            {
                return;
            }

            hasCollision = true;
            lastCollision = other.gameObject;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer != Mask)
            {
                return;
            }

            hasCollision = false;
        }
    }
}