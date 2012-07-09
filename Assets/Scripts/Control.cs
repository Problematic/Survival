using UnityEngine;
using System.Collections.Generic;

public class Control : MonoBehaviour {
	
	public Man man;
	
	public s_Tree tree;
	public Camera currentCamera;
	
	public MouseBlocker blocker;
	
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

		gui.GetItems().ForEach( (i) => blocker.AddRect(i.name, i.rect) );
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRaycast();
		
		Vector3 focus = man.transform.position;
		currentCamera.transform.position = new Vector3(focus.x, 
														currentCamera.transform.position.y, 
														focus.z - 15f);
		
		if (placing != null) {
			placing.FollowCursor(info.point);
		}
	}
	
	private bool UpdateRaycast() {
		return Physics.Raycast(currentCamera.ScreenPointToRay(Input.mousePosition), out info);
	}
	
	public void ClickEvent() {
		if (!blocker.MouseIsBlocked()) {
			if (placing == null) {
				MoveMan(info.point);
									
				// Fix detecting clicking on a resource
				Resource res = info.collider.GetComponent("Resource") as Resource;
				
				if (res != null) {
						Debug.Log ("HARVESTING");
						Harvest(res);
				}
			} else {
				placing.Build();
				placing = null;
			}
		}
	}
	
	public void ClickEvent (Gui.button button) {
		switch(button) {
			case Gui.button.Build_House:
				placing = Instantiate(buildable_house, info.point, new Quaternion(-1.0f, 0f, 0f, 1f)) as Buildable;
				break;
		}
	}
	
	public void RemoveGUIRect(string name) {
		blocker.RemoveRect(name);
	}
	
	public void AddGUIRect(string name, Rect rect) {
		blocker.AddRect(name, rect);
	}
	
	
	//============= test methods ============
	public void Harvest(Resource res) {
		man.AddToInventory(new Wood(), 10);
	}
	
	public void MoveMan(Vector3 WorldLocation) {
		man.move(WorldLocation);
	}
	
	public void Build(Buildable thing) {
		thing.Build();
	}
	
	public void SpawnTree() {
//		s_Tree obj = Instantiate(tree, info.point, Quaternion.identity) as s_Tree;
	}
}
