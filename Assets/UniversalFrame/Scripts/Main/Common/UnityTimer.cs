using System;
using UnityEngine;

public class UnityTimer
{
    private float _curTime;
    private float _time;
    private Action _action;

    public void Start(float v,Action action)
    {
        _time = v;
        _curTime = 0;
        _action = action;
        if (v <= 0)
            return;
        Timers.Inst.AddUpdate(Update);
    }

    private void Update(object ob)
    {
        _curTime += Time.deltaTime;
        if (_curTime >= _time)
        {
            _curTime = 0;
            _action?.Invoke();
            Stop();
        }
    }

    public void Stop()
    {
        Timers.Inst.Remove(Update);
    }
}
