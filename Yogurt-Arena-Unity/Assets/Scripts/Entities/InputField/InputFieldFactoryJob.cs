using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct InputFieldFactoryJob
    {
        public async UniTask<InputFieldAspect> Run()
        {
            InputConfig inputConfig = Query.Single<Config>().Input;
            InputFieldView inputFieldView = await inputConfig.Asset.Spawn();

            InputFieldAspect inputField = World.Create()
                .AddLink(inputFieldView.gameObject)
                .Add(inputConfig)
                .Add(inputFieldView)
                .Add(inputFieldView.MoveInputReader)
                .Add(new InputState())
                .As<InputFieldAspect>();

            new UpdateMoveInputJob().Run(inputField);

            return inputField;
        }
    }
}