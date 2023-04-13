using Cysharp.Threading.Tasks;

namespace Yogurt.Arena
{
    public class RifleFactoryJob
    {
        public async UniTask<ItemAspect> Run(AgentAspect owner)
        {
            RifleData data = Query.Single<Data>().Rifle;
            
            return await new ItemFactoryJob().Run(owner, new UseRifleJob(), data);
        }
    }
}