using Cysharp.Threading.Tasks;
using DG.Tweening;

namespace Yogurt.Arena
{
    public struct AnimateLocationAppearanceJob
    {
        public async UniTask Run(LocationPartTag location)
        {
            if (Query.Single<Level>().Current == 0)
            {
                return;
            }
                
            location.transform.DOMoveY(-100, 0);

            float appearTime = 1f;
            location.transform.DOMoveY(0, appearTime).SetEase(Ease.InOutExpo);

            await Wait.Seconds(appearTime);
        }
    }
}