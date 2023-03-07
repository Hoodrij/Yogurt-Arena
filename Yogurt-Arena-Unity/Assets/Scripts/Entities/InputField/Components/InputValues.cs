using UnityEngine;

namespace Yogurt.Arena
{
    public class InputValues : IComponent
    {
        public Vector2 MoveDelta;

        public Vector2 CumulativeVelocity;
    }
}