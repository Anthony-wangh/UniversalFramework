using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    /// <summary>
    /// 不绘制 只响应事件
    /// </summary>
    [AddComponentMenu("UI/TransparentRaycast")]
    public class TransparentRaycast : MaskableGraphic
    {
        protected TransparentRaycast()
        {
            useLegacyMeshGeneration = false;
        }

        protected override void OnPopulateMesh(VertexHelper toFill)
        {
            toFill.Clear();
        }
    }
}