using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena
{
    public class Config : ScriptableObject
    {
        [SerializeReference]
        public List<ScriptableObject> All;
    }
}