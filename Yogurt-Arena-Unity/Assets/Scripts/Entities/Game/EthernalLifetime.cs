using System.Threading;
using UnityEngine;

namespace Yogurt.Arena
{
    public class EthernalLifetime : IComponent
    {
        public static implicit operator CancellationToken(EthernalLifetime lifetime)
        {
            return Application.exitCancellationToken;
        } 
    }
}