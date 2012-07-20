using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Gui : MonoBehaviour {

	public Man man;
	public Mouse mouse;
	public Control control;
	public ActionQueue queue;
	
	public delegate void DrawGuiElement(GuiObjectInfo g);
	public delegate void OpenWindowTask(GuiObjectInfo g);
	public OpenWindowTask NewWindowTask;
	public GuiObjectInfo NewWindow;
	
	public class GuiObjectInfo {
		public string name;
		public string text;
		public Rect rect;
		public List<GuiObjectInfo> children;
		public event DrawGuiElement Draw;
		public GuiObjectInfo(Rect r, string s, string t = "") {
			name = s;
			rect = r;
			text = t;
			children = new List<GuiObjectInfo>();
			Draw += (g) => GUI.Box(g.rect, g.text);
		}
		public GuiObjectInfo(Rect r, DrawGuiElement d, string s, string t = "") {
			name = s;
			rect = r;
			text = t;
			children = new List<GuiObjectInfo>();			
			Draw += d;
		}
		public void DrawElement() {
			if (Draw == null) {
				GUI.Box(rect, text)	;
			} else {
				Draw(this);	
			}
		}
		public void DrawAllChildren() {
			GUI.BeginGroup(rect);
			foreach (GuiObjectInfo child in children) {child.Draw(child);}
			GUI.EndGroup();
		}
		public List<GuiObjectInfo> GetChildren() {return children;}
		public void SetChildren(List<GuiObjectInfo> list) {children = list;}
		public void AddChild(GuiObjectInfo child) {children.Add(child);}
		public void SetDrawAction(DrawGuiElement d) {Draw = d;}
	};
	
	void Start () {
		BuildGUI();
		GuiItems.Add(BuildInventoryWindow());
	}
	
	void OnGUI () {
		NewWindowTask = null;
		NewWindow = null;
		if (GuiItems != null) {
			foreach (GuiObjectInfo g in GuiItems) {
				g.DrawElement();
			}
			
			if (NewWindowTask != null) {
				NewWindowTask(NewWindow);
			}
		}
		
		if (control.mousetarget != null) {
			WorldObject t = control.mousetarget;
		}
	}
	public List<GuiObjectInfo> GuiItems;
		
	public void BuildGUI() {
		GuiItems = new List<GuiObjectInfo>();
		
		
		GuiItems.Add(BuildGraphs());
		OpenWindow(BuildToolBar());
	}
	
	public GuiObjectInfo BuildToolBar() {
		int x = 300, y = 0, w = 400, h = 50;
		GuiObjectInfo bar = new GuiObjectInfo(new Rect(x, y, w, h), "MainToolBar", "");
		bar.AddChild(new GuiObjectInfo(new Rect(5, 5, 40, 40), 
									(g) => {if (GUI.Button(g.rect, g.text)) {
											NewWindowTask = ToggleWindow;
											NewWindow = BuildInventoryWindow();
									}},
									"LeftPane", "Loot")); 
		
//		bar.AddChild(new GuiObjectInfo(new Rect(50, 5, 40, 40), 
//									(g) => {if (GUI.Button(g.rect, g.text)) {
//											NewWindowTask = ToggleWindow;
//											//NewWindow = BuildBenchUpgradeWindow(man.knowledge);
//									}},
//									"LeftPane", "Craft")); 
		
		bar.Draw += (g) => {
			GUI.Box(g.rect, g.text);
			g.DrawAllChildren();
		};
		return bar;
	}
	
	public GuiObjectInfo BuildGraphs() {
		int x = 700, y = 0, w = 200, h = 50;
		GuiObjectInfo box = new GuiObjectInfo(new Rect(x, y, w, h), "GraphsBox", "");
		box.AddChild(new GuiObjectInfo(new Rect(0, 0 , w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().health / 100.0f), g.rect.height), "");
			},
			"HealthBar", "Health")); 
		
		box.AddChild(new GuiObjectInfo(new Rect( w / 2 + 3, 0 , w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().hunger / 100.0f), g.rect.height), "");
			},
			"HungerBar", "Hunger")); 
		
		box.AddChild(new GuiObjectInfo(new Rect(0, 5 + (h - 5)/2, w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().energy / 100.0f), g.rect.height), "");
			},
			"EnergyBar", "Energy")); 
		
		box.AddChild(new GuiObjectInfo(new Rect(w / 2 + 3, 5 + (h - 5)/2, w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().thirst / 100.0f), g.rect.height), "");
			},
			"ThirstBar", "Thirst")); 
		
		box.Draw += (g) => {
			GUI.Box(g.rect, "");
			g.DrawAllChildren();
		};	
		return box;
	}
	
	public GuiObjectInfo BuildInventoryWindow() {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, Screen.height-400, 270, 400), "LeftPane", "Inventory");
		
		int tileWidth = 265;
		int tileHeight = 20;
		int buttonSide = 20;
		
		window.Draw += (g) =>
		{
			GUI.Box(g.rect, g.text);
			int num = 1;
			var d = man.GetComponent<Inventory>().GetInventory();
		foreach(var i in d) {
				GUI.Box(new Rect(
								5,
								num * (tileHeight + 5),
								tileWidth,
								tileHeight
							),
							i.Key.name + " -- " + i.Value);
//				if (GUI.Button(new Rect(10 + tileWidth, num * (tileHeight + 5), buttonSide, buttonSide), "!")) {
//					WorldObject t = i.GetTarget();
//					if (t==null) {
//						control.UseEvent(i);
//					} else {
//						control.UseEvent(i, t);
//					}
//				}
				num++;
			};
		};

		window.Draw += (g) => {
			g.DrawAllChildren();
		};
		
		return window;
	}
	
	public GuiObjectInfo BuildBenchUpgradeWindow(Table t, Knowledge k) {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "LeftPane", "Upgrade Bench");	
		
		window.Draw += (g) => {
			int num = 0, boxY = 30;
			foreach (Bench b in k.benches) {
				boxY = num * 65 + 30;
				GUI.Box(new Rect(5, boxY, 290, 60), "");
				GUI.Label(new Rect(10, 5 + boxY, 200, 25), b.customname);
				GUI.Label(new Rect(10, 30 + boxY, 150, 25), b.description);
				GUI.Label(new Rect(210, 5 + boxY, 150, 25), b.buildcost[0].toString());
				
				if (GUI.Button(new Rect(240, 30 + boxY, 45, 25), "Build")) {
					queue.Enqueue(control.SimpleAction(
						(d) => {
							t.bench = b;
							OpenWindow(BuildBenchWindow(b));
						}));
				}
				num++;
			}
		};

		return window;
	}

	public GuiObjectInfo BuildBenchWindow(Bench bench) {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "LeftPane", bench.customname);
		
		window.Draw += (g) => {
			int i, o, num = 0;
			foreach(CraftingConversion cc in bench.craftables) {
				i = o = num;
				GUI.Label(new Rect(130, 20 + num * 30, 60, 25), "------>");
				foreach (ResourceCount r in cc.reqs) {
					GUI.Box(new Rect(5, 20 + i * 30, 70, 25), r.amount + " " + r.r.name);
					i++;
				}
				foreach (ResourceCount y in cc.yields) {
					GUI.Box(new Rect(225, 20 + o * 30, 70, 25), y.amount + " " + y.r.name);
					o++;
				}
				num += Mathf.Max(o, i);
			}
		};
		
		return window;
	}
	
	private bool WindowIsOpen(string name, out int index) {
		int i = 0;
		foreach(GuiObjectInfo g in GuiItems) {
			index = i;
			if (g.name == name) {
				return true;
			}
			i++;
		}
		index = -1;
		return false;
	}
	
	public void OpenWindow(GuiObjectInfo window) {
		int index;
		if (WindowIsOpen(window.name, out index)) GuiItems.RemoveAt(index);	
		GuiItems.Add(window);
		control.AddGUIRect(window.name, window.rect);
	}
	
	public void CloseWindow(GuiObjectInfo window) {
		int index;
		if (!WindowIsOpen(window.name, out index)) return;
		GuiItems.RemoveAt(index);
		control.RemoveGUIRect(window.name);
	}
	
	private void ToggleWindow(GuiObjectInfo window) {
		int index;
		if(WindowIsOpen(window.name, out index)) {
			GuiItems.RemoveAt(index);
			control.RemoveGUIRect(window.name);
		} else {
			GuiItems.Add(window);
			control.AddGUIRect(window.name, window.rect);
		}
	}
	
	public List<GuiObjectInfo> GetItems() {
		return GuiItems;
	}
}
