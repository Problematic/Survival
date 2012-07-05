using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public Man man;
	public Camera camera;
	
	private enum state {
		NONE, 
		ON_GUI, 
		BUILDING,
		MOVING_TO_TASK
	};
	
	private state current_state;
	
	private Buildable placing;
	
	private Gui gui;
	private RaycastHit info;
	
	// Use this for initialization
	void Start () {
		current_state = state.NONE;
		gui = camera.GetComponent("Gui") as Gui;
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRaycast();
		if (placing != null) {
			placing.FollowCursor(info.point);
		}
	}
	
	private bool UpdateRaycast() {
		return Physics.Raycast(camera.ScreenPointToRay(Input.mousePosition), out info);
	}
	
	public void ClickEvent() {

			MoveMan(info.point);
								
			// Fix detecting clicking on a resource
			Resource res = info.collider.GetComponent("Resource") as Resource;
			
			if (res != null) {
					Harvest(res);
			}
	}
	
	public void ClickEvent (Gui.button button) {
		switch(button) {
			case Gui.button.Build_House:
				break;
		}
	}
	
	public void Harvest(Resource res) {
		int amount = res.Harvest();
		string type = res.GetType();
		
		man.AddResource(type, amount);
		
		if (res.IsEmpty()) {
			Component c = res as Component;
			DestroyObject(c.gameObject);
		}
	}
	
	public void MoveMan(Vector3 WorldLocation) {
		man.move(WorldLocation);
	}
	
	public void Build(Buildable thing) {
		thing.Build();
	}
}
