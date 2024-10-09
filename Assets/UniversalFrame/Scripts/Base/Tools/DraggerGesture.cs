using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Action = System.Action;


public enum GestureState
{
    None = 0,
    Up = 2,
    Down = 4,
    Left = 8,
    Right = 16,
    DownLeft,
    DownRight,
    UpLeft,
    UpRight,

    LeftUp,
    LeftDown,
    RightUp,
    RightDown,



    Invalid
}
public enum Direction
{
    None,
    Up,
    Down,
    Left,
    Right
}
/// <summary>
/// 手势集合
/// </summary>
public class GestureContainer
{
    public static Dictionary<GestureState, List<Direction>> Gestures = new()
    {
    { GestureState.Down , new() { Direction.Down }},
    { GestureState.Up , new(){ Direction.Up }},
    { GestureState.Left , new() { Direction.Left }},
    { GestureState.Right , new() { Direction.Right }},
    { GestureState.DownLeft , new() { Direction.Down, Direction.Left}},
    { GestureState.DownRight , new() { Direction.Down,Direction.Right }},
    { GestureState.UpLeft , new() { Direction.Up,Direction.Left }},
    { GestureState.UpRight , new() { Direction.Up,Direction.Right }},
    { GestureState.LeftDown , new() { Direction.Left, Direction.Down }},
    { GestureState.LeftUp , new() { Direction.Left, Direction.Up }},
    { GestureState.RightDown , new() {Direction.Right, Direction.Down }},
    { GestureState.RightUp , new(){Direction.Right, Direction.Up }},
    };

}
public interface IDraggerEvent
{
    void AddEvent(GestureState state, Action callBack);
    void RemoveEvent(GestureState state, Action callBack);
}
/// <summary>
/// 手势识别
/// </summary>
public class DraggerGesture : Singleton<DraggerGesture>, IDraggerEvent
{
    private Dictionary<GestureState, List<Action>> _callBack;
    private readonly Dictionary<GestureState, string> _tipList = new() {
        {GestureState.None ,"无效手势"},{GestureState.Invalid ,"无效手势"},
        {GestureState.Down ,"向下滑动"}, { GestureState.Up ,"向上滑动"},
        { GestureState.Left, "向左滑动" }, { GestureState.Right ,"向右滑动"},
        { GestureState.DownLeft ,"后退"},{ GestureState.DownRight ,"向下右滑动"},
        { GestureState.UpLeft ,"向上左滑动"},{ GestureState.UpRight ,"向上右滑动"},
    };
    private GestureState _state = GestureState.None;
    private Vector2 _preMousePoint;
    private Direction _preDirection;
    private List<Direction> _curDirections;
    public void Init()
    {
        _callBack = new Dictionary<GestureState, List<Action>>();
        InputManager.BeginDrag += OnBeginDrag;
        InputManager.EndDrag += OnEndDrag;
        InputManager.Drag += OnDrag;
    }
    private void OnBeginDrag()
    {
        _state = GestureState.None;
        _preMousePoint = InputManager.Pos;
        _curDirections = new List<Direction>();
        _preDirection = Direction.None;
    }
    private void OnDrag()
    {
        UpdateGestures();
        UpdateTip();
        if (!_isGather)
        {
            _isGather = true;
            Timers.Inst.StartCoroutine(UpdatePrePos());
        }
    }

    private bool _isGather;
    IEnumerator UpdatePrePos()
    {
        yield return new WaitForSeconds(0.2f);
        _preMousePoint = InputManager.Pos;
        _isGather = false;
    }
    private Direction GetDirection()
    {
        var dir = GetDragDirection();
        var dotH = Vector2.Dot(Vector2.right, dir);
        var dorV = Vector2.Dot(Vector2.up, dir);
        //更趋向于横向滑动
        if (Mathf.Abs(dotH) > Mathf.Abs(dorV))
        {
            return dotH > 0 ? Direction.Right : Direction.Left;
        }

        if (Mathf.Abs(dotH) < Mathf.Abs(dorV))
        {
            return dorV > 0 ? Direction.Up : Direction.Down;
        }

        return _preDirection;
    }

    private void UpdateGestures()
    {
        var dir = GetDirection();
        if (_preDirection != dir)
        {
            _preDirection = dir;
            _curDirections.Add(dir);
            _state = MatchGesture();
        }
    }
    

    private GestureState MatchGesture()
    {
        var state = GestureState.Invalid;
        foreach (var gesture in GestureContainer.Gestures)
        {
            if (gesture.Value.SequenceEqual(_curDirections))
            {
                state = gesture.Key;
                break;
            }
        }

        return state;
    }


    private void OnEndDrag()
    {
        if (_callBack.TryGetValue(_state, out var actionList))
        {
            for (int i = actionList.Count - 1; i >= 0; i--)
            {
                actionList[i]?.Invoke();
            }
        }
        //Tip.Inst.Dispose();
    }


    private Vector2 GetDragDirection()
    {
        var v = (Vector2)Input.mousePosition - _preMousePoint;
        return v.normalized;
    }

    private void UpdateTip()
    {
        //Tip.Inst.SetTip(_state.ToString());
    }
    public void AddEvent(GestureState state, Action callBack)
    {
        if (_callBack.TryGetValue(state, out var value))
        {
            value.Add(callBack);
            return;
        }
        var list = new List<Action> { callBack };
        _callBack.Add(state, list);
    }

    public void RemoveEvent(GestureState state, Action callBack)
    {
        if (_callBack.TryGetValue(state, out var value))
        {
            value.Remove(callBack);
        }
    }
}
