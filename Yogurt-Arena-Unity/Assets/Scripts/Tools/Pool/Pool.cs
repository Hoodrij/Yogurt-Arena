using System;
using System.Collections.Generic;
using UnityEngine;

namespace Yogurt.Arena.Tools
{
    public class Pool
    {
        private Stack<GameObject> pool = new();
        private Func<Awaitable<GameObject>> factory;

        public Pool(Func<Awaitable<GameObject>> factory)
        {
            this.factory = factory;
        }
        
        public async Awaitable<GameObject> Pop()
        {
            GameObject go;
            if (pool.Count <= 0)
            {
                go = await factory.Invoke();
                PoolLink link = go.AddComponent<PoolLink>();
                link.Pool = this;
            }
            else
            {
                go = pool.Pop();
                if (go == null)
                {
                    return await Pop();
                }
            }

            go.SetActive(true);
            return go;
        }

        public void Push(GameObject go)
        {
            pool.Push(go);
            go.SetActive(false);
        }
    }
}