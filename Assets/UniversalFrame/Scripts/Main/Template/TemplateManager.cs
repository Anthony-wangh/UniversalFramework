using System.Collections;
using System.Diagnostics;
using Framework.Core;
using Framework.Core.Interface;
using UnityEngine;
using UnityEngine.Networking;
using Debug = UnityEngine.Debug;

namespace Assets.Scripts.Runtime.Common
{
    /// <summary>
    /// 模板表管理
    /// </summary>
    public class TemplateManager : AbstractBase, IManager
    {
        //模板表路径
        private  readonly string _templatePath = "Template/TempleteConfig.json";

        public Config Config
        {
            get
            {
                if (_config==null)
                {
                    Debug.Log("模板表为空，请检查配置文件格式是否正确！");
                }
                return _config;
            }
        }

        private Config _config;

        public void Init()
        {
            Timers.Inst.StartCoroutine(Load());
        }
        
        #region 初始化配置
        
        private  IEnumerator Load()
        {
            //LogUtility.Log("程序开始初始化");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string filepath = Application.streamingAssetsPath + "/" + _templatePath;
            UnityWebRequest webRequest = new UnityWebRequest(filepath) { downloadHandler = new DownloadHandlerBuffer() };
            yield return webRequest.SendWebRequest();
            if (!string.IsNullOrEmpty(webRequest.error) || string.IsNullOrEmpty(webRequest.downloadHandler.text))
            {
                //LogUtility.Log("加载模板配置:" + webRequest.error);
                yield break;
            }
            sw.Stop();
            //LogUtility.Log("加载模板表数据完成！加载时间："+sw.ElapsedMilliseconds);
            _config = JsonUtility.FromJson<Config>(webRequest.downloadHandler.text);
            //TemplateGlobal.ParseTemplate(_config);
            Publish(new AppStartMessage() { AppStart=true});
        }
        #endregion

        
    }
}
