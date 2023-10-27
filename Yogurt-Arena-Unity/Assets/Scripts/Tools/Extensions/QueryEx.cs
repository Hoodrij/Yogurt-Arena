using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public static class QueryEx
    {
        public static void ListenAdded<TAspect>(this QueryOfAspect<TAspect> query, Action<TAspect> callback, Entity lifetime = default) where TAspect : struct, IAspect
        {
            if (!lifetime.Exist)
            {
                lifetime = Query.Of<Game>().Single();
            }

            HashSet<Entity> players = new();
            lifetime.Run(Update);
            return;

            
            void Update()
            {
                foreach (TAspect aspect in query)
                {
                    if (players.Add(aspect.Entity))
                    {
                        callback.Invoke(aspect);
                    }
                }
            }
        }
        
        public static void ListenAdded<TAspect>(this QueryOfAspect<TAspect> query, Func<TAspect, UniTaskVoid> callback, Entity lifetime = default) where TAspect : struct, IAspect
        {
            if (!lifetime.Exist)
            {
                lifetime = Query.Of<Game>().Single();
            }

            HashSet<Entity> players = new();
            lifetime.Run(Update);
            return;

            
            void Update()
            {
                foreach (TAspect aspect in query)
                {
                    if (players.Add(aspect.Entity))
                    {
                        callback.Invoke(aspect);
                    }
                }
            }
        }
    }
}