using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Yogurt.Arena
{
    public class InvertedMaskImage : Image
    {
        private static readonly int stencilComp = Shader.PropertyToID("_StencilComp");

        public override Material materialForRendering
        {
            get
            {
                Material result = base.materialForRendering;
                result.SetInt(stencilComp, (int)CompareFunction.NotEqual);
                return result;
            }
        }
    }
}