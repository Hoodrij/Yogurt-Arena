using UnityEngine;

namespace Yogurt.Arena
{
    public struct InputFieldFactoryJob
    {
        public async Awaitable Run()
        {
            InputData inputData = Query.Single<Data>().Input;
            InputFieldView inputFieldView = await inputData.Asset.Spawn();

            Entity entity = World.Create()
                .AddLink(inputFieldView.gameObject)
                .Add(inputData)
                .Add(inputFieldView)
                .Add(inputFieldView.MoveInputReader)
                .Add<InputState>();
        }
    }
}