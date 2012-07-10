using UnityEngine;
using System.Collections;

public interface IAction {

    ActionState state { get; }
    //called once before any calls to update
    void Begin();
    //called once per frame
    void Update();
    //called after action is done
    void End();
    //called to stop action early
    void Kill();
}

public enum ActionState {
    NotStarted,
    Running,
    Done,
}