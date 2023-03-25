using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public struct InputFieldFactoryJob
    {
        private Assets Assets => Query.Single<Assets>();
        
        public async UniTask Run()
        {
            InputFieldView inputFieldView = await Assets.InputField.Spawn();

            Entity entity = World.Create()
                .AddLink(inputFieldView.gameObject)
                .Add(inputFieldView)
                .Add(inputFieldView.MoveInputReader)
                .Add<InputState>();
        }
    }
}