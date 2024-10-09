using System.Collections.Generic;
using System.IO;
using System.Text;
using Framework.Extension;
using UnityEditor;
using UnityEngine;

namespace Assets.DevFramework.Scripts.Editor
{
    /// <summary>
    /// 自动生成UI组件缓存脚本
    /// </summary>
    public class AutoGetUIComponents
    {
        public static GameObject m_viewGoRoot;
        public static List<string> m_properityList = new List<string>();
        public static List<string> m_getCompentList = new List<string>();

        private static string _path = "Assets/Scripts/Main/View";
        private static string _templateText;


        [MenuItem("GameObject/MVC/UIDesign", priority = 2)]
        public static void AutoGetUIComponentsWindows()
        {
            var selectObj = Selection.activeObject;
            if (selectObj == null || !(selectObj is GameObject gameObj) || !gameObj.name.Contains("View"))
            {
                EditorUtility.DisplayDialog("警告！", "请先选中界面预制体！", "确定");
                return;
            }
            _templateText = selectObj.name.Replace("View", "");
            _path = PlayerPrefs.GetString(EditorPath.MvcTemplatePathKey, _path);

            _path = _path + "/" + _templateText;
            if (!Directory.Exists(_path))
            {
                EditorUtility.DisplayDialog("警告！", "生成UI组件设计脚本之前请先创建MVC模板！", "确定");
                return;
            }
            m_viewGoRoot = (GameObject)selectObj;
            Generate(m_viewGoRoot);
        }


        private static void Generate(GameObject root)
        {
            m_viewGoRoot = root;
            if (root == null)
                return;
            var children = root.GetComponentsInChildren<Transform>(true);
            if (children == null || children.Length == 0)
                return;
            foreach (var item in children)
            {
                TsNeedAddInViewElement(item);
            }

            CreateDesignTemplate(_templateText);
            AssetDatabase.Refresh();
        }

        static void TsNeedAddInViewElement(Transform childts)
        {
            string properitystr = "";
            string tempgetCompentstr = "";
            string properityName = childts.name;
            if (childts.name.Contains("_Txt"))
            {
                tempgetCompentstr = "Text";
                properitystr = "public Text " + properityName + ";";

            }
            else if (childts.name.Contains("_Tog"))
            {
                tempgetCompentstr = "Toggle";
                properitystr = "public Toggle " + properityName + ";";
            }
            else if (childts.name.Contains("_Btn"))
            {
                tempgetCompentstr = "Button";
                properitystr = "public Button " + properityName + ";";
            }
            else if (childts.name.Contains("_RawImg"))
            {
                tempgetCompentstr = "RawImage";
                properitystr = "public RawImage " + properityName + ";";
            }
            else if (childts.name.Contains("_Img"))
            {
                tempgetCompentstr = "Image";
                properitystr = "public Image " + properityName + ";";
            }
            else if (childts.name.Contains("_Ts"))
            {
                tempgetCompentstr = "Transform";
                properitystr = "public Transform " + properityName + ";";
            }
            else if (childts.name.Contains("_RectTs"))
            {
                tempgetCompentstr = "RectTransform";
                properitystr = "public RectTransform " + properityName + ";";
            }
            else if (childts.name.Contains("_Input"))
            {
                tempgetCompentstr = "InputField";
                properitystr = "public InputField " + properityName + ";";
            }
            else if (childts.name.Contains("_Sld"))
            {
                tempgetCompentstr = "Slider";
                properitystr = "public Slider " + properityName + ";";
            }
            if (!string.IsNullOrEmpty(properitystr))
            {
                m_properityList.Add(properitystr);
                string path = childts.GetPath(m_viewGoRoot.transform);
                string tempgetCompentNameStr = string.Format(properityName + " = ViewObj.FindChild<{0}>(" + '"' + path + '"' + ");", tempgetCompentstr);
                m_getCompentList.Add(tempgetCompentNameStr);
            }

        }

        public static void CreateDesignTemplate(string templateName)
        {
            Debug.Log("创建Design模版");
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using Framework.Extension;");
            sb.AppendLine("using UnityEngine.UI;");
            sb.AppendLine("using UnityEngine;");
            sb.AppendLine();
            sb.AppendLine();
            sb.AppendLine("namespace " + _path.Replace(@"/", @"."));
            sb.AppendLine("{");

            sb.AppendFormat("   public partial class {0} ", templateName + "View");
            sb.AppendLine();
            sb.AppendLine("   {");
            foreach (var item in m_properityList)
            {
                sb.AppendLine("        " + item);
            }
            sb.AppendLine();
            sb.AppendLine("        public override void InitComponents()");
            sb.AppendLine("        {");
            foreach (var item in m_getCompentList)
            {
                sb.AppendLine("            " + item);
            }
            sb.AppendLine("        }");
            sb.AppendLine();
            sb.AppendLine("   }");
            sb.AppendLine("}");

            CreateScript(_path + @"\" + templateName + "View.Design.cs", sb.ToString());
        }
        /// <summary>保存脚本到View目录下</summary>
        private static void CreateScript(string path, string msg)
        {
            StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8);
            sw.Write(msg);
            sw.Flush();
            sw.Close();
        }
    }
}
