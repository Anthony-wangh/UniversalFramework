using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using Framework.Extension;

/// <summary>
/// 创建 Text、Image 的时候默认不选中 raycastTarget 等
/// </summary>
public class CreateUIMenuEditor
{
    /// <summary>
    /// 第一次创建UI元素时，没有canvas、EventSystem所有要生成，Canvas作为父节点
    /// 之后再空的位置上建UI元素会自动添加到Canvas下
    /// 在非UI树下的GameObject上新建UI元素也会 自动添加到Canvas下（默认在UI树下）
    /// 添加到指定的UI元素下
    /// </summary>
    [MenuItem("GameObject/UI/Image")]
    static void CreatImages()
    {
        var canvasObj = SecurityCheck();

        if (!Selection.activeTransform)      // 在根目录创建的， 自动移动到 Canvas下
        {
            // Debug.Log("没有选择对象");
            Image().transform.SetParent(canvasObj.transform);
        }
        else
        {
            if (!Selection.activeTransform.GetComponentInParent<Canvas>())    // 没有在UI树下
            {
                Image().transform.SetParent(canvasObj.transform);
            }
            else
            {
                Image();
            }
        }
    }

    private static GameObject Image()
    {
        GameObject go = new GameObject("Image", typeof(Image));
        go.GetComponent<Image>().raycastTarget = false;
        go.layer = LayerMask.NameToLayer("UI");
        go.transform.SetParent(Selection.activeTransform,false);
        Selection.activeGameObject = go;
        return go;
    }


    // 我们要设置默认字体
    [MenuItem("GameObject/UI/Text")]
    static void CreatTexts()
    {
        var canvasObj = SecurityCheck();

        if (!Selection.activeTransform)      // 在根目录创建的， 自动移动到 Canvas下
        {
            Text().transform.SetParent(canvasObj.transform);
        }
        else
        {
            if (!Selection.activeTransform.GetComponentInParent<Canvas>())    // 没有在UI树下
            {
                Text().transform.SetParent(canvasObj.transform);
            }
            else
            {
                Text();
            }
        }
    }
    
    private static GameObject Text()
    {
        GameObject go = new GameObject("Text", typeof(TextPlus));
        go.layer = LayerMask.NameToLayer("UI");
        var text = go.GetComponent<Text>();
        text.raycastTarget = false;
        text.supportRichText = false;
        text.SetSizeDeltaY(40);
        text.alignment = TextAnchor.MiddleLeft;
        text.fontSize = 24;
        //text.font = AssetDatabase.LoadAssetAtPath<Font>("Assets/ZhiBenJia/Resources/Font/WnQuanYi.ttf");   // 默认字体
        go.transform.SetParent(Selection.activeTransform,false);
        Selection.activeGameObject = go;

        return go;
    }


    // 如果第一次创建UI元素 可能没有 Canvas、EventSystem对象！
    private static GameObject SecurityCheck()
    {
        GameObject canvasObj;
        var cc = Object.FindObjectOfType<Canvas>();
        if (!cc)
        {
            canvasObj = new GameObject("Canvas", typeof(Canvas));
            Canvas canvas = canvasObj.GetComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvasObj.AddComponent<CanvasScaler>();
            canvasObj.AddComponent<GraphicRaycaster>();
        }
        else
        {
            canvasObj = cc.gameObject;
        }
        if (!Object.FindObjectOfType<EventSystem>())
        {
            new GameObject("EventSystem", typeof(EventSystem));
        }

        return canvasObj;
    }
}

