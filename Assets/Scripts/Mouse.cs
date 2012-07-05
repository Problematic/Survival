using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	public Camera currentCamera;
	public Control control;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			control.ClickEvent();
		}
	}
}
