using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct OvermindFactoryJob
    {
        public async UniTask<OvermindAspect> Run()
        {
            OvermindAspect overmind = World.Create()
                .Add(new GetConfigJob().Run<OvermindConfig>())
                .Add(new OvermindState())
                .As<OvermindAspect>();

            new UpdateOvermindDestinationJob().Run(overmind);

            return overmind;
        }
    }
}