using System.Threading;

namespace Yogurt.Arena;

public class Game : IComponent
{
    public static CancellationToken Token => Application.exitCancellationToken;
    public static Lifetime Life { get; private set; }

    public Game()
    {
        Life = Application.exitCancellationToken;
    }
}