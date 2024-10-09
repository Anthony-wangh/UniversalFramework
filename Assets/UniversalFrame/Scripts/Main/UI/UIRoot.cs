using Framework.Core;
using Framework.Core.Message;
using UnityEngine;

public class UIRoot : MonoSingleton<UIRoot>
{
    public Canvas Canvas;

    private MessageManager _message;

    [Header("触发菜单动画的滑动距离")]
    public float OpenMenuOffset = 200;
    private void Awake()
    {
        Canvas = GetComponentInChildren<Canvas>();
    }

    
    private void Start()
    {
        InputManager.BeginDrag += BeginDrag;
        InputManager.EndDrag += EndDrag;
        _message = Facade.Inst.GetManager<MessageManager>();
        _message.Subscribe<AppStartMessage>(OnAppStart);
    }

    private void OnAppStart(AppStartMessage obj)
    {
        if (!obj.AppStart)
            return;
        _main = Canvas.transform.Find("Main").GetRectrf();
        var y = _main.rect.height;
        _main.anchoredPosition = new Vector2(_main.anchoredPosition.x, y);
    }

    private void OnDestroy()
    {
        InputManager.BeginDrag -= BeginDrag;
        InputManager.EndDrag -= EndDrag;
        _message.Unsubscribe<AppStartMessage>(OnAppStart);
    }

    private Vector2 _pos;
    private RectTransform _main;
    private void BeginDrag()
    {
        _pos = InputManager.Pos;
    }
    private void EndDrag()
    {
        var offset = InputManager.Pos - _pos;
        if (offset.y > OpenMenuOffset)
        {
            ShowPanel(false);
        }
        else if(offset.y < -OpenMenuOffset)
        {
            ShowPanel(true);
        }
    }


    private void ShowPanel(bool show)
    {
        if (_main == null)
            return;
        //_message.Publish(new EnterEditorMessage());
        //var y = show ? 0 : _main.rect.height;
        //_main.DOAnchorPos(new Vector2(_main.anchoredPosition.x, y), 1).SetEase(Ease.InOutBack);
    }
    
}
