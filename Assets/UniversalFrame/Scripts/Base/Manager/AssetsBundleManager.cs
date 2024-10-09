using UnityEngine;

namespace DevFramework.Scripts.Common
{
    public class AssetsBundleManager
    {
        private const string MainBundleName = "AssetBundles";
        private const string SpritesBundleName = "sprite";
        private const string ShaderBundleName = "shader";
        private const string RoleBundleName = "role";
        private const string SkinBundleName = "skintextures";
        private const string TextureBundleName = "materialtexture";


        private static string MainDic => $"{Application.streamingAssetsPath}/{MainBundleName}/";

        private static AssetBundle roleBundle;
        private static AssetBundle skinBundle;
        private static AssetBundle atlasBundle;
        
        public static void Init()
        {
            //Ԥ���ؽ�������-����ͼƬ
            //AssetBundle.LoadFromFile($"{MainDic}{SpritesBundleName}");
            //Ԥ���ز�������-Shader
            //AssetBundle.LoadFromFile($"{MainDic}{ShaderBundleName}");

        }
        /// <summary>
        ///��ɫģ��
        /// </summary>
        /// <returns></returns>
        public static AssetBundle GetRoleModel()
        {
            if (roleBundle == null)
            {
                //Ԥ���ؽ�ɫģ��
                roleBundle = AssetBundle.LoadFromFile($"{MainDic}{RoleBundleName}");
            }

            return roleBundle;
        }


        /// <summary>
        /// ����Ƥ������
        /// </summary>
        public static AssetBundle GetSkinBundle()
        {
            if (skinBundle==null)
            {
                skinBundle=AssetBundle.LoadFromFile(MainDic+SkinBundleName);
            }
            return skinBundle;
        }


        /// <summary>
        /// ����ͼ��
        /// </summary>
        /// <returns></returns>
        public static AssetBundle GetTextureBundle()
        {
            if (atlasBundle==null)
            {
                atlasBundle=AssetBundle.LoadFromFile(MainDic+ TextureBundleName);
            }

            return atlasBundle;
        }
    }
}
