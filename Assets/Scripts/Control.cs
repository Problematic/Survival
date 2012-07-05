using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public Man man;
	public Camera currentCamera;
	
	public House buildable_house; 
	
	private enum state {
		NONE, 
		ON_GUI, 
		BUILDING,
		MOVING_TO_TASK
	};
	
	private state current_state;
	
	private Buildable placing;
	
	private Gui gui;
	private bool onGui;
	
	private RaycastHit info;
	
	// Use this for initialization
	void Start () {
		current_state = state.NONE;
		gui = currentCamera.GetComponent<Gui>();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRaycast();
		if (placing != null) {
			placing.FollowCursor(info.point);
		}
	}
	
	private bool UpdateRaycast() {
		return Physics.Raycast(currentCamera.ScreenPointToRay(Input.mousePosition), out info);
	}
	
	public void ClickEvent() {
		if (!onGui) {
			if (placing == null) {
				MoveMan(info.point);
									
				// Fix detecting clicking on a resource
				Resource res = info.collider.GetComponent("Resource") as Resource;
				
				if (res != null) {
						Harvest(res);
				}
			} else {
				placing.Build();
				placing = null;
			}
		}
	}
	
	public void ClickEvent (Gui.button button) {
		onGui = true;
		switch(button) {
			case Gui.button.Build_House:
				placing = Instantiate(buildable_house, info.point, Quaternion.identity) as Buildable;
				break;
		}
	}
	
	public void Harvest(Resource res) {
		int amount = res.Harvest();
		string type = res.GetResourceType();
		
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
