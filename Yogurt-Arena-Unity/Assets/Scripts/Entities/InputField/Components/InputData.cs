using UnityEngine;

namespace Yogurt.Arena
{
    public class InputData : IComponent
    {
        public Vector2 MoveDelta;

        public Vector2 CumulativeVelocity;
    }
}