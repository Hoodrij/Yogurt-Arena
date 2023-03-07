using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public class InputFieldView : MonoBehaviour, IDisposable, IComponent
    {
        public MoveInputReader MoveInputReader;

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}