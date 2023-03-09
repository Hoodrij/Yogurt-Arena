using UnityEngine;

namespace Yogurt.Arena
{
    public class InputState : IComponent
    {
        public Vector2 MoveDelta;

        public Vector2 CumulativeVelocity;
    }
}