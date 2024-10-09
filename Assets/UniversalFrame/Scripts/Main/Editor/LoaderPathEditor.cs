using UnityEditor;
using UnityEngine;

public class LoaderPathEditor
{
    [MenuItem("Framework/LoaderPath")]
    public static void CreateWindows()
    {
        EditorWindow.GetWindow<LoaderPathEditorWindow>("����Ԥ��·������");
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

        EditorGUILayout.LabelField("Resources���ص�Ԥ��·����");
        EditorGUILayout.Space();
        _viewPrefabs = EditorGUILayout.TextField("����Ԥ����·����", _viewPrefabs);
        EditorGUILayout.Space();
        _viewAssets = EditorGUILayout.TextField("������Դ·����", _viewAssets);
        EditorGUILayout.Space();
        _prefabs = EditorGUILayout.TextField("Ԥ����·����", _prefabs);
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("�ű���Ԥ��·����");
        _mvcTemplate = EditorGUILayout.TextField("MVCģ��·����", _mvcTemplate);
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("ȡ��"))
        {
            Close();
        }
        if (GUILayout.Button("ȷ��"))
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
