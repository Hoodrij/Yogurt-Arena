using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct OvermindFactoryJob
    {
        public async UniTask<OvermindAspect> Run()
        {
            OvermindAspect overmind = World.Create()
                .Add(Query.Single<Data>().Overmind)
                .Add<OvermindState>()
                .As<OvermindAspect>();

            new RunOvermindBehaviorJob().Run(overmind);

            return overmind;
        }
    }
}