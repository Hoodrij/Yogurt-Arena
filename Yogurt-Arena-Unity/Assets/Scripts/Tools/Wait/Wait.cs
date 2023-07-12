using System;
using UnityEngine;

namespace Yogurt.Arena
{
    public static class Wait
    {
        public static async Awaitable While(Func<bool> predicate)
        {
            while (predicate())
            {
                await Awaitable.NextFrameAsync();
            }
        }
        
        public static async Awaitable Until(Func<bool> predicate)
        {
            while (!predicate())
            {
                await Awaitable.NextFrameAsync();
            }
        }

        public static Awaitable Update()
        {
            return Awaitable.NextFrameAsync();
        }

        public static Awaitable Seconds(float seconds)
        {
            return Awaitable.WaitForSecondsAsync(seconds);
        }

        public static async Awaitable Any(params Awaitable[] tasks)
        {
            bool haveCompletedTask = false;

            while (!haveCompletedTask)
            {
                foreach (Awaitable awaitable in tasks)
                {
                    if (awaitable.IsCompleted)
                    {
                        haveCompletedTask = true;
                        break;
                    }
                }

                await Awaitable.NextFrameAsync();
            }
            
            foreach (Awaitable awaitable in tasks)
            {
                awaitable.Cancel();
            }
        }
        
        public static async Awaitable All(params Awaitable[] tasks)
        {
            bool allTasksCompleted = false;

            while (!allTasksCompleted)
            {
                allTasksCompleted = true;
                foreach (Awaitable awaitable in tasks)
                {
                    allTasksCompleted &= awaitable.IsCompleted;
                }

                await Awaitable.NextFrameAsync();
            }
        }
    }
}