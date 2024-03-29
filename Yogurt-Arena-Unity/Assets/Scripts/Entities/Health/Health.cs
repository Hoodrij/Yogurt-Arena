﻿using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public class Health : IComponent
    {
        public int MaxHealth;
        public int Value;
        public HealthWidget HealthWidget;
        public IDeathJob DeathJob;
    }

    public interface IDeathJob
    {
        public UniTaskVoid Run(Entity entity);
    }
}