using UnityEngine;
using System.Collections;
using System;

public class DAction : IAction {

    public event Action<DAction> OnBegin;
    public event Action<DAction> OnUpdate;
    public event Action<DAction> OnEnd;
    public event Action<DAction> OnKill;

    public ActionState state {
        get;
        set;
    }
    public void Begin() {
        state = ActionState.Running;
        OnBegin.Call(this);
    }
    public void Update() {
        OnUpdate.Call(this);
    }
    public void End() {
        OnEnd.Call(this);
    }
    public void Kill() {
        OnEnd.Call(this);
        state = ActionState.Done;
    }

    Action<DAction> FromEnumerator(IEnumerator coroutine) {
        return da => {
            if (!coroutine.MoveNext()) da.state = ActionState.Done;
        };
    }
}
