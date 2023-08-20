using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct OvermindFactoryJob
    {
        public async UniTask<OvermindAspect> Run()
        {
            OvermindAspect overmind = World.Create()
                .Add(Query.Single<Config>().Overmind)
                .Add(new OvermindState())
                .As<OvermindAspect>();

            new UpdateOvermindDestinationJob().Run(overmind);

            return overmind;
        }
    }
}