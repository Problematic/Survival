using UnityEngine;
using System.Collections.Generic;

public class Control : MonoBehaviour {
	
	public Man man;
	
//	public s_Tree tree;
	public Camera currentCamera;
	
	public MouseBlocker blocker;
	
	public House buildable_house;
	
	public WorldObject mousetarget;
	
	private enum state {
		NONE, 
		ON_GUI, 
		BUILDING,
		MOVING_TO_TASK
	};
	
	private IBuildable placing;
	
	private Gui gui;
	private bool onGui;
	
	private RaycastHit info;
	
	// Use this for initialization
	void Start () {
		gui = currentCamera.GetComponent<Gui>();

		gui.GetItems().ForEach( (i) => blocker.AddRect(i.name, i.rect) );
	
		ControlData.init(man, this);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRaycast();
		
		mousetarget = info.collider.GetComponent<WorldObject>() as WorldObject;
		
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
				var h = info.collider.GetComponent<Harvestable>();
				
				if (h != null) {
					Debug.Log ("HARVESTING");
					Harvest(h);
				}
			} else {
				placing.Build();
				placing = null;
			}
		}
	}
	
	public void RemoveGUIRect(string name) {
		blocker.RemoveRect(name);
	}
	
	public void AddGUIRect(string name, Rect rect) {
		blocker.AddRect(name, rect);
	}
	
	public void UseEvent(InventoryItem item, WorldObject target = null) {
		if (target == null) {
			item.UseAction();
		} else {
			item.UseAction(target);
		}
	}
	
	//============= test methods ============
	public void Harvest(Harvestable h) {
		man.Harvest(h);
	}
	
	public void MoveMan(Vector3 WorldLocation) {
		man.move(WorldLocation);
	}
	
	public void Build(IBuildable thing) {
//		placing = Instantiate(buildable_house, info.point, new Quaternion(-1, 0, 0, 1)) as IBuildable;
	}
	
	public void SpawnTree() {
//		s_Tree obj = Instantiate(tree, info.point, Quaternion.identity) as s_Tree;
	}
}
