
// ReSharper disable InconsistentNaming

using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 创建MVC的三个类模版
///     如果选中文件夹是View目录的下一级目录，那么会创建文件脚本（如果已经存在脚本会给出是否替换的确认框）
///     如果不是选中View目录的下一级目录，那么会弹出创建模版的窗口
/// </summary>
public class CreateMVCTemplate
{
    public static string TemplatePath;
    private static string _path = "Assets/Scripts/View";
    [MenuItem("GameObject/MVC/Template", priority = 1)]
    public static void CreateTemplate()
    {
        var selectObj = Selection.activeObject;
        if (selectObj == null || !(selectObj is GameObject gameObj) || !gameObj.name.Contains("View"))
        {
            Debug.Log("请先选中需要创建MVC模板的界面预制体");
            return;
        }
        TemplatePath = selectObj.name.Replace("View", "");
        _path = PlayerPrefs.GetString(EditorPath.MvcTemplatePathKey, _path);

        string path = _path + "/" + TemplatePath;
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        CreateTemplates(path, TemplatePath);
    }





    /// <summary>
    /// 创建模版到对应路径下
    ///     如果文件夹名已经存在判断是否有MVC的模版脚本，如果有弹出是否替换确认框
    ///     如果没有那么就直接创建文件夹并创建脚本。
    /// </summary>
    public static bool CreateTemplates(string path, string templateName)
    {
        bool isSuccess = false;
        if (File.Exists(path + @"\" + templateName + "View.cs") ||
            File.Exists(path + @"\" + templateName + "Model.cs") ||
            File.Exists(path + @"\" + templateName + "Control.cs"))//已经存在文件，提示是否替换
        {
            if (EditorUtility.DisplayDialog("警告！", "已经存在相同的脚本文件，是否替换！", "确定", "取消"))
            {
                TemplatePath = path;
                CreateViewTemplate(templateName);
                CreateModelTemplate(templateName);
                CreateControllerTemplate(templateName);
                CreateEventTemplate(templateName);
                AssetDatabase.Refresh();
                isSuccess = true;
            }
        }
        else
        {
            TemplatePath = path;
            CreateViewTemplate(templateName);
            CreateModelTemplate(templateName);
            CreateControllerTemplate(templateName);
            CreateEventTemplate(templateName);
            AssetDatabase.Refresh();
            isSuccess = true;
        }

        Selection.activeObject = AssetDatabase.LoadAssetAtPath<Object>(path);//聚焦文件夹
        return isSuccess;
    }

    public static void CreateViewTemplate(string templateName)
    {
        Debug.Log("创建View模版");
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using UnityEngine;");
        sb.AppendLine("using Framework.Core;");
        sb.AppendLine();
        sb.AppendLine();

        sb.AppendLine("namespace " + TemplatePath.Replace(@"/", @"."));
        sb.AppendLine("{");

        sb.AppendFormat("   public partial class {0} : ViewBase ", templateName + "View");
        sb.AppendLine();
        sb.AppendLine("   {");
        sb.AppendLine();
        sb.AppendLine("        protected override void OnInit()");
        sb.AppendLine("        {");
        sb.AppendLine("            ");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        protected override void OnShow()");
        sb.AppendLine("        {");
        sb.AppendLine("            ");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        protected override void OnClose()");
        sb.AppendLine("        {");
        sb.AppendLine("            ");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        protected override void OnSubscribe()");
        sb.AppendLine("        {");
        sb.AppendLine("            ");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        protected override void OnUnsubscribe()");
        sb.AppendLine("        {");
        sb.AppendLine("            ");
        sb.AppendLine("        }");
        sb.AppendLine();

        sb.AppendLine("   }");
        sb.AppendLine("}");

        CreateScript(TemplatePath + @"\" + templateName + "View.cs", sb.ToString());
    }

    public static void CreateModelTemplate(string templateName)
    {
        Debug.Log("创建Model模版");
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using Framework.Core;");
        sb.AppendLine();
        sb.AppendLine();

        sb.AppendLine("namespace " + TemplatePath.Replace(@"/", @"."));
        sb.AppendLine("{");

        sb.AppendFormat("   public class {0} : ModelBase ", templateName + "Model");
        sb.AppendLine();
        sb.AppendLine("   {");
        sb.AppendLine("       ");
        sb.AppendLine("   }");
        sb.AppendLine("}");

        CreateScript(TemplatePath + @"\" + templateName + "Model.cs", sb.ToString());

    }

    public static void CreateControllerTemplate(string templateName)
    {
        Debug.Log("创建Controller模版");
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using Framework.Core;");
        sb.AppendLine("using Framework.Managers;");
        sb.AppendLine();
        sb.AppendLine();

        sb.AppendLine("namespace " + TemplatePath.Replace(@"/", @"."));
        sb.AppendLine("{");

        sb.AppendFormat("   public class {0} : ControlBase", templateName + "Control");
        sb.AppendLine();
        sb.AppendLine("   {");
        sb.AppendLine("        private ViewManager _manager;");
        sb.AppendLine("        protected override void OnInit()");
        sb.AppendLine("        {");
        sb.AppendLine("            _manager = GetManager<ViewManager>();");
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("        protected override void OnSubscribe()");
        sb.AppendLine("        {");
        sb.AppendFormat("            Subscribe<{0}ViewOpenMessage>(OnEventTrigger);", templateName);
        sb.AppendLine();
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendFormat("        private void OnEventTrigger({0}ViewOpenMessage eventMessage)", templateName);
        sb.AppendLine();
        sb.AppendLine("        {");
        sb.AppendLine("            if(eventMessage.IsOpen)");
        sb.AppendFormat("               _manager.OpenView<{0}View>();", templateName);
        sb.AppendLine();
        sb.AppendLine("            else");
        sb.AppendFormat("               _manager.CloseView<{0}View>();", templateName);
        sb.AppendLine();
        sb.AppendLine("        }");
        sb.AppendLine();
        sb.AppendLine("       ");
        sb.AppendLine("   }");
        sb.AppendLine("}");

        CreateScript(TemplatePath + @"\" + templateName + "Control.cs", sb.ToString());

    }


    public static void CreateEventTemplate(string templateName)
    {
        Debug.Log("创建Event模版");
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("using Framework.Core.Message;");
        sb.AppendLine();
        sb.AppendLine();
        sb.AppendLine("namespace " + TemplatePath.Replace(@"/", @"."));
        sb.AppendLine("{");

        sb.AppendFormat("   public struct {0}", templateName + "ViewOpenMessage: IMessageBase");
        sb.AppendLine();
        sb.AppendLine("   {");
        sb.AppendFormat("        public bool IsOpen;");
        sb.AppendLine();
        sb.AppendLine("   }");
        sb.AppendLine("}");

        CreateScript(TemplatePath + @"\" + templateName + "ViewMessage.cs", sb.ToString());
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


