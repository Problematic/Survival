using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	public Man man;
	public Mouse mouse;
	public Move move;
	public Control control;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void OnGUI () {
		string vect = man.nextmove.ToString();
		GUI.Box(new Rect(10, 10, 120, 40), "Position:\n" + man.pos.ToString());
		GUI.Box(new Rect(130, 10, 120, 40), "Destination:\n" + mouse.hit.ToString());
		GUI.Box(new Rect(250, 10, 120, 40), "Wood:\n" + man.wood);
		
		if (GUI.Button(new Rect(10, 55, 80, 20), "Build house")) {
			//control.
		}

	}
}
