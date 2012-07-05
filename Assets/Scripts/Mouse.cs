using UnityEngine;
using System.Collections;

public class Mouse : MonoBehaviour {
	
	public Camera camera;
	public Control control;
	public Vector3 hit;
	
	private Ray ray;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)) {
			ray = camera.ScreenPointToRay(Input.mousePosition);			
			RaycastHit info;
			if (Physics.Raycast(ray, out info)) {
				hit = info.point;
				control.MoveMan(info.point);
					
				// Fix detecting clicking on a resource
				Resource res = info.collider.GetComponent("Resource") as Resource;
				
				if (res != null) {
						control.Harvest(res);
				}
					
				Debug.DrawRay(ray.origin, info.point, Color.red);	
				Debug.DrawRay(ray.origin, ray.direction * 100.0f, Color.green);
			}
		}
	}
}
