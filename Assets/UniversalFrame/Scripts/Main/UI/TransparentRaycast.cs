using UnityEngine;
using UnityEngine.UI;

namespace Framework.UI
{
    /// <summary>
    /// ������ ֻ��Ӧ�¼�
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