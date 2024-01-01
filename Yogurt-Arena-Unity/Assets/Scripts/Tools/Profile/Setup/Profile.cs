using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Profiling;

namespace Yogurt
{
    public struct Profile : IDisposable
    {
        public static Dictionary<string, Profile> Cache = new Dictionary<string, Profile>();
        public static ProfilerMarker.AutoScope Start(string name)
        {
            if (!Cache.TryGetValue(name, out Profile profile))
            {
                profile = new Profile(name);
                Cache.Add(name, profile);
            }
            return profile.marker.Auto();
        }

        private const double TO_MS = 1e-6f;
        private const ProfilerRecorderOptions RECORD_OPTIONS = ProfilerRecorderOptions.WrapAroundWhenCapacityReached
                                                               | ProfilerRecorderOptions.CollectOnlyOnCurrentThread;
        private ProfilerMarker marker;
        private ProfilerRecorder recorder;
        
        private Profile(string name)
        {
            marker = new ProfilerMarker(name);
            recorder = ProfilerRecorder.StartNew(marker, 100, RECORD_OPTIONS);
        }
        
        public void Dispose()
        {
            recorder.Dispose();
        }
        
        public override string ToString()
        {
            return $"{GetValue():F6} ms";
        }
        
        private unsafe double GetValue()
        {
            int samplesCount = recorder.Capacity;
            if (samplesCount == 0)
                return 0;

            double sum = 0;

            var samples = stackalloc ProfilerRecorderSample[samplesCount];
            recorder.CopyTo(samples, samplesCount);
            for (int i = 0; i < samplesCount; ++i)
            {
                if (samples[i].Value == 0) 
                    continue;
                sum += samples[i].Value;
            }

            return sum / samplesCount * TO_MS;
        }
    }
}