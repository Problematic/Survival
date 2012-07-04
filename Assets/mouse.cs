using UnityEngine;
using System.Collections;

public class mouse : MonoBehaviour {
	
	public Camera camera;
	
	private Ray ray;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			ray = camera.ScreenPointToRay(Input.mousePosition);
			Debug.DrawRay(ray.origin, ray.direction*1000.0f, Color.red);	
		}
	}
}
