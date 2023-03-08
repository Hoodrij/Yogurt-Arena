using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public class AgentView : MonoBehaviour, IComponent, IDisposable
    {
        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}