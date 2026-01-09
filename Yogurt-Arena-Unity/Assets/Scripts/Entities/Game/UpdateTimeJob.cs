namespace Yogurt.Arena;

public struct UpdateTimeJob
{
    public void Run(GameAspect game)
    {
        game.Run(Loop);
        return;

        void Loop()
        {
            game.Time.Delta = UnityEngine.Time.deltaTime;
            game.Time.Scale = game.Time.Delta / game.Time.ExpectedDelta;
        }
    }
}