using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MouseBlocker : MonoBehaviour {

    List<IMouseBlocker> mouseBlockers = new List<IMouseBlocker>();
    Dictionary<string, Rect> otherBlockers = new Dictionary<string, Rect>();
	
    public bool debug;


    public bool MouseIsBlocked() {
		Vector3 mouse = Input.mousePosition;
		mouse.y = Screen.height-mouse.y;
        foreach (IMouseBlocker blocker in mouseBlockers) {
            Rect r = blocker.GetCurrentRect();
			if (r.Contains(mouse)) {
				return true;
            }
        }

        foreach (Rect r in otherBlockers.Values) {
            if (r.Contains(mouse)) {
                return true;
            }
        }
		return false;
    }

    public void OnGUI() {
		if (!debug)
			return;
//		GUI.skin = CM.GUISkin;
		GUI.color = new Color(0, 0, 0, 0.5f);
        foreach (IMouseBlocker blocker in mouseBlockers) {
			GUI.Label(blocker.GetCurrentRect(), "", "SolidBox");
		}
        foreach(Rect blocker in otherBlockers.Values) {
            GUI.Label(blocker, "", "SolidBox");
        }
	}

    public void AddRect(string key, Rect rect) {
        if (!otherBlockers.ContainsKey(key)) {
            Debug.Log(" added rect " + key + "["+rect.ToString()+"]");
            otherBlockers.Add(key, rect);
        }
        else {
            otherBlockers[key] = rect;
        }
    }

    public void RemoveRect(string key) {
        if (otherBlockers.ContainsKey(key)) {
            Debug.Log("removing  rect " + key + "[" + otherBlockers[key].ToString() + "]");
            otherBlockers.Remove(key);
        }
    }

    public void Register(IMouseBlocker blocker) {
        Debug.Log("Registering Rect" + blocker.GetCurrentRect().x + " / " + blocker.GetCurrentRect().y + blocker.GetCurrentRect().width + " / " + blocker.GetCurrentRect().height + "for blocker " + blocker.GetType());
        mouseBlockers.Add(blocker);
    }

    public void UnRegister(IMouseBlocker blocker) {
        Debug.Log("UnRegistering Rect" + blocker.GetCurrentRect().x + " / " + blocker.GetCurrentRect().y + blocker.GetCurrentRect().width + " / " + blocker.GetCurrentRect().height);
        mouseBlockers.Remove(blocker);
    }
}
