using System;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;

namespace Yogurt.LoadTests
{
    public class Test_Load
    {
        [OneTimeTearDown]
        public void TearDownOnce() => ProfileResult.Start();

        // [Repeat(100)]
        [TestCase(1, 0)]
        [TestCase(1, 1)]
        [TestCase(100, 50)]
        public void CreateAndKill(int createCount, int killCount)
        {
            WorldBridge.World?.Dispose();
            
            for (int i = 0; i < createCount; i++)
            {
                Entity entity = Entity.Create();
                if (i >= createCount - killCount)
                {
                    entity.Kill();
                }
            }
            WorldBridge.UpdateWorld();

            int totalEntities = createCount - killCount;
            Assert.IsTrue(WorldBridge.World.Entities.Count == totalEntities);
        }
        
        [TestCase(1)]
        [TestCase(100)]
        public void Create(int count)
        {
            for (int i = 0; i < count; i++)
            {
                Entity.Create();
            }

            Assert.IsTrue(WorldBridge.World.Entities.Count == count);
        }
    }
}