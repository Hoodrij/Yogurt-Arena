namespace Yogurt.Arena;

public struct GameFactoryJob
{
    public async UniTask<Entity> Run()
    {
        Entity game = Entity.Create()
            .Add(new Game())
            .Add(new Time());

        new LoadConfigsJob().Run(game);
        return game;
    }
}