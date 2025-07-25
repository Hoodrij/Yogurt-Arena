using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct InputFieldFactoryJob
    {
        public async UniTask<InputFieldAspect> Run()
        {
            InputConfig inputConfig = new GetConfigJob().Run<InputConfig>();
            InputFieldView inputFieldView = await inputConfig.Asset.Spawn();

            InputFieldAspect inputField = World.Create()
                .Link(inputFieldView.gameObject)
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