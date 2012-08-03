using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Control : MonoBehaviour {
	
	public Man man;	
	public Peon[] peons;
	public World world;
	private Combat combat;
	public Camera currentCamera;
	public MouseBlocker blocker;
	
	public House buildable_house;
	
	public WorldObject mousetarget;
	
	private TreeGUIRenderer gui;
	
	private RaycastHit info;
	
	// Use this for initialization
	void Start () {
		gui = Static.GUI;
		
		gui.GetItems().ForEach( (i) => blocker.AddRect(i.name, i.rect) );
	
		ControlData.init(man, this);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateRaycast();
		
		mousetarget = info.collider.GetComponent<WorldObject>();
		
		Vector3 focus = man.transform.position;
		currentCamera.transform.position = new Vector3(focus.x, 
														currentCamera.transform.position.y, 
														focus.z - 15f);
	}
	
	private bool UpdateRaycast() {
		return Physics.Raycast(currentCamera.ScreenPointToRay(Input.mousePosition), out info);
	}
	
	public void ClickEvent() {
		if (!blocker.MouseIsBlocked()) {
				
				man.queue.CancelAll();
				MoveMan(info.point);
				
				man.targetObject = info.collider.GetComponent<WorldObject>();
				
				// Fix detecting clicking on a resource
				var h = info.collider.GetComponent<Harvestable>();
				if (h != null) {
					Harvest(h);
					return;
				}
				var t = info.collider.GetComponent<Table>();
				if (t != null ) {
					man.queue.Enqueue(SimpleAction(
						(d) => {
							if (t.bench == null) {
								gui.OpenWindow(gui.BuildBenchUpgradeWindow(t, man.knowledge));
							} else {
								gui.OpenWindow(gui.BuildBenchWindow(t.bench));
							}
							d.state = ActionState.Done;
						}));
				}
				var f = info.collider.GetComponent<HearthFire>();
				if (f != null) {
					man.queue.Enqueue(SimpleAction(
						(d) => {
							gui.OpenWindow(gui.BuildHearthFireWindow(f));
							d.state = ActionState.Done;
						}
					));
				}
			
				var a = info.collider.GetComponent<Armory>();
				if (a != null) {
					man.queue.Enqueue(SimpleAction(
						(d) => {
							gui.OpenWindow(gui.BuildArmoryWindow(a));
							d.state = ActionState.Done;
						}
					));
				} else {
					var bin = info.collider.GetComponent<ItemBin>();
					if (bin != null) {
						man.queue.Enqueue(SimpleAction(
							(d) => {
								gui.OpenWindow(gui.BuildBinWindow(bin));
								d.state = ActionState.Done;
							}
						));
					}
				}
				
		}
	}
	
	public void WorldEvent(World.WorldEvents w) {
		switch (w) {
		case World.WorldEvents.NightStarted:
			if (Static.Man.AtFire) return;
			Static.Man.transform.position = Static.HearthFire.transform.position + Vector3.right * 2f + Vector3.up;
			float angle = Mathf.PI/4f;
			foreach (var p in peons) {
					p.transform.position = Static.HearthFire.transform.position + 
										   new Vector3(Mathf.Cos(angle) , 0f, Mathf.Sin(angle)) * 2f
										   + Vector3.up;
					angle += Mathf.PI/4f;
			}
			man.UpdateCombatStats();
			gui.OpenWindow(gui.BuildCombatWindow(man, new Genericenemy(), combat));
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
	
	public DAction SimpleAction(Action<DAction> act) {
		DAction da = new DAction();
		
		da.OnUpdate += act;
		
		return da;
	}
	
	public void Harvest(Harvestable h) {
		man.Harvest(h);
	}
	
	public void MoveMan(Vector3 WorldLocation) {
		man.MoveTo(WorldLocation);
	}	
}
