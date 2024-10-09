using UnityEditor;
using UnityEngine;

public class LoaderPathEditor
{
    [MenuItem("Framework/LoaderPath")]
    public static void CreateWindows()
    {
        EditorWindow.GetWindow<LoaderPathEditorWindow>("工程预设路径设置");
    }
}

public class LoaderPathEditorWindow : EditorWindow
{
    private string _viewPrefabs = "View";
    private string _viewAssets = "ViewAssets";
    private string _prefabs = "Prefabs";

    private string _mvcTemplate = "Assets/Scripts/View";

    private void OnEnable()
    {
        _viewPrefabs = PlayerPrefs.GetString(ResourceManager.ViewPathKey, _viewPrefabs);
        _viewAssets = PlayerPrefs.GetString(ResourceManager.ViewUIPathKey, _viewAssets);
        _prefabs = PlayerPrefs.GetString(ResourceManager.PrefabsPathKey, _prefabs);
        _mvcTemplate = PlayerPrefs.GetString(EditorPath.MvcTemplatePathKey, _mvcTemplate);
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("Resources加载的预设路径：");
        EditorGUILayout.Space();
        _viewPrefabs = EditorGUILayout.TextField("界面预制体路径：", _viewPrefabs);
        EditorGUILayout.Space();
        _viewAssets = EditorGUILayout.TextField("界面资源路径：", _viewAssets);
        EditorGUILayout.Space();
        _prefabs = EditorGUILayout.TextField("预制体路径：", _prefabs);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("脚本的预设路径：");
        _mvcTemplate = EditorGUILayout.TextField("MVC模板路径：", _mvcTemplate);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("取消"))
        {
            Close();
        }
        if (GUILayout.Button("确定"))
        {
            PlayerPrefs.SetString(ResourceManager.ViewPathKey, _viewPrefabs);
            PlayerPrefs.SetString(ResourceManager.ViewUIPathKey, _viewAssets);
            PlayerPrefs.SetString(ResourceManager.PrefabsPathKey, _prefabs);
            PlayerPrefs.SetString(EditorPath.MvcTemplatePathKey, _mvcTemplate);
            Close();
        }
        EditorGUILayout.EndHorizontal();
        EditorGUILayout.Space();
        
        EditorGUILayout.EndVertical();
    }
}
