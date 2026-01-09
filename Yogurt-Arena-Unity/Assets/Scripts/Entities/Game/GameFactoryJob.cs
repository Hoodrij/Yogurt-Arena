namespace Yogurt.Arena;

public struct GameFactoryJob
{
    public async UniTask<GameAspect> Run()
    {
        GameAspect game = Entity.Create()
            .Add(new Game())
            .Add(new Time())
            .As<GameAspect>();

        new UpdateTimeJob().Run(game);
        new LoadConfigsJob().Run(game);
        return game;
    }
}