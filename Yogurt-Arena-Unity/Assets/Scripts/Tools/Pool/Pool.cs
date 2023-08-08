using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Yogurt.Arena.Tools
{
    public class Pool
    {
        private Stack<GameObject> pool = new();
        private Func<UniTask<GameObject>> factory;

        public Pool(Func<UniTask<GameObject>> factory)
        {
            this.factory = factory;
        }
        
        public async UniTask<GameObject> Pop()
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