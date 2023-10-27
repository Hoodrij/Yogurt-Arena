namespace Yogurt.Arena.Analytics
{
    public struct FireAnalyticsEventJob
    {
        static int i;
        
        public void Run()
        {
            i++.log();
        }
    }
}