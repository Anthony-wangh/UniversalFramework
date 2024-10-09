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
            //预加载界面依赖-精灵图片
            //AssetBundle.LoadFromFile($"{MainDic}{SpritesBundleName}");
            //预加载材质依赖-Shader
            //AssetBundle.LoadFromFile($"{MainDic}{ShaderBundleName}");

        }
        /// <summary>
        ///角色模型
        /// </summary>
        /// <returns></returns>
        public static AssetBundle GetRoleModel()
        {
            if (roleBundle == null)
            {
                //预加载角色模型
                roleBundle = AssetBundle.LoadFromFile($"{MainDic}{RoleBundleName}");
            }

            return roleBundle;
        }


        /// <summary>
        /// 加载皮肤纹理
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
        /// 加载图集
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
