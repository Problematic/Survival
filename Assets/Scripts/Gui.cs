using UnityEngine;
using System.Collections;

public class Gui : MonoBehaviour {

	public Man man;
	public Mouse mouse;
	public Move move;
	public Control control;
	
	public enum button {
		Build_House	
	};
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void OnGUI () {
		string vect = man.nextmove.ToString();
		GUI.Box(new Rect(250, 10, 120, 40), "Wood:\n" + man.wood);
		
		if (GUI.Button(new Rect(10, 55, 80, 20), "Build House")) {
			control.ClickEvent(button.Build_House);
		}

	}
}
