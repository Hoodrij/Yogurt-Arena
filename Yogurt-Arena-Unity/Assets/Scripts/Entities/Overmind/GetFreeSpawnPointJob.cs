namespace Yogurt.Arena;

public struct GetFreeSpawnPointJob
{
    public async UniTask<Vector3> Run(OvermindAspect overmind)
    {
        NavMeshSurface level = Query.Single<Location>().NavSurface;
        PlayerAspect player = Query.Single<PlayerAspect>();
        Vector3 result = Vector3.zero;

        await Wait.Until(HasFreeSpawnPoint, overmind.Life());
        return result;
            
        bool HasFreeSpawnPoint()
        {
            Vector3 randomPoint = level.navMeshData.sourceBounds.GetRandomPoint().WithY(0);
            NavMesh.SamplePosition(randomPoint, out var hit, 100, NavMesh.AllAreas);
            Vector3 randomPointOnNavMesh = hit.position;

            if (IsPlayerFarEnough(randomPointOnNavMesh))
            {
                result = randomPointOnNavMesh;
                return true;
            }

            return false;
        }
        bool IsPlayerFarEnough(Vector3 randomPoint)
        {
            if (!player.Exist())
                return true;
                
            Vector3 playerPosition = player.Agent.Body.Position;
            float distanceToPlayer = (playerPosition - randomPoint).magnitude;

            return distanceToPlayer > overmind.Config.MinRangeToPlayer;
        }
    }
}