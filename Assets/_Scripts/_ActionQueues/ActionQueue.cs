using UnityEngine;
using System.Collections.Generic;

public class ActionQueue : MonoBehaviour {

    Queue<IAction> actions = new Queue<IAction>();

    public void Enqueue(IAction action) {
        actions.Enqueue(action);
    }

    void FixedUpdate() {
        if(actions.Count == 0) return;
        var act = actions.Peek();
        if(act.state == ActionState.NotStarted)
            act.Begin();
        act.Update();
        if(act.state == ActionState.Done) {
            act.End();
            actions.Dequeue();
        }
    }

    public void CancelCurrent() {
        if(actions.Count == 0) return;
        var act = actions.Dequeue();
        if(act.state == ActionState.Running) act.Kill();
    }

    public void CancelAll() {
        CancelCurrent();
        actions.Clear();
    }
}
