using System.Threading;
using UnityEngine;

namespace Yogurt.Arena
{
    public class EthernalLifetime : IComponent
    {
        private CancellationTokenSource cts;

        public EthernalLifetime()
        {
            cts = new();
            Application.quitting += Kill;
        }

        public void Kill()
        {
            cts.Cancel();
            Application.quitting -= Kill;
        }
        
        public static implicit operator CancellationToken(EthernalLifetime lifetime)
        {
            return lifetime.cts.Token;
        }
        
    }
}