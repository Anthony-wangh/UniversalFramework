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
    /// ģ������
    /// </summary>
    public class TemplateManager : AbstractBase, IManager
    {
        //ģ���·��
        private  readonly string _templatePath = "Template/TempleteConfig.json";

        public Config Config
        {
            get
            {
                if (_config==null)
                {
                    Debug.Log("ģ���Ϊ�գ����������ļ���ʽ�Ƿ���ȷ��");
                }
                return _config;
            }
        }

        private Config _config;

        public void Init()
        {
            Timers.Inst.StartCoroutine(Load());
        }
        
        #region ��ʼ������
        
        private  IEnumerator Load()
        {
            //LogUtility.Log("����ʼ��ʼ��");
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string filepath = Application.streamingAssetsPath + "/" + _templatePath;
            UnityWebRequest webRequest = new UnityWebRequest(filepath) { downloadHandler = new DownloadHandlerBuffer() };
            yield return webRequest.SendWebRequest();
            if (!string.IsNullOrEmpty(webRequest.error) || string.IsNullOrEmpty(webRequest.downloadHandler.text))
            {
                //LogUtility.Log("����ģ������:" + webRequest.error);
                yield break;
            }
            sw.Stop();
            //LogUtility.Log("����ģ���������ɣ�����ʱ�䣺"+sw.ElapsedMilliseconds);
            _config = JsonUtility.FromJson<Config>(webRequest.downloadHandler.text);
            //TemplateGlobal.ParseTemplate(_config);
            Publish(new AppStartMessage() { AppStart=true});
        }
        #endregion

        
    }
}
