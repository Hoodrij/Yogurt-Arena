using System;
using System.Threading;
using UnityEngine;

namespace Yogurt.Arena
{
    public class EthernalLifetime : IComponent
    {
        public CancellationTokenSource Cts;

        public EthernalLifetime()
        {
            Cts = new();
            GameObject gameObject = new()
            {
                hideFlags = HideFlags.HideAndDontSave
            };
            LifetimeBehavior behavior = gameObject.AddComponent<LifetimeBehavior>();
            behavior.Lifetime = this;
        }
        
        public static implicit operator CancellationToken(EthernalLifetime lifetime)
        {
            return lifetime.Cts.Token;
        }
        
    }

    class LifetimeBehavior : MonoBehaviour
    {
        public EthernalLifetime Lifetime;

        private void OnApplicationQuit()
        {
            Lifetime.Cts.Cancel();
        }
    }
}