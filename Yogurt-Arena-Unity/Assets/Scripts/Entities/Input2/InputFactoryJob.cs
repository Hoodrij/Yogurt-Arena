namespace Yogurt.Arena
{
    public class InputFactoryJob
    {
        public InputAspect Run()
        {
            InputConfig inputConfig = new GetConfigJob().Run<InputConfig>();

            InputAspect input = World.Create()
                .Add(inputConfig)
                .Add(new InputState())
                .As<InputAspect>();

            new UpdateInput2Job().Run(input);

            return input;
        }
    }
}