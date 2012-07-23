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
	
	public Color green = Color.green;
	public Color red = Color.red;
	
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
		
		bar.AddChild(new GuiObjectInfo(new Rect(50, 5, 3f /Time.deltaTime, 40), "FPS", "FPS: " + 1f /Time.deltaTime));
		
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
			int num = 0, boxY = 30, inc = 0;
			foreach (Bench b_ in k.benches) {
				
				var b = b_;
				inc = 0;
				int[] inv = man.GetComponent<Inventory>().GetAmounts(b.buildcost);
				int boxH = 30 + inv.Length * 25;
				GUI.Box(new Rect(5, boxY, 290, boxH), "");
				GUI.Label(new Rect(10, 5 + boxY, 200, 25), b.customname);
				GUI.Label(new Rect(10, boxY + boxH - 25, 150, boxH - 30), b.description);
				
				int i = 0;
				bool canbuild = true;
				foreach(ResourceCount rc in b.buildcost) {
					
					SetColor(() => {return inv[i] >= rc.amount;});
					if (inv[i] < rc.amount) {canbuild = false;}
					
					GUI.Label(new Rect(210, 5 + boxY + inc, 150, 25), inv[i] + "/" + rc.amount + " " + rc.r.customname);
					inc += 20;
					i++;
				}
				
				SetColor();
				GUI.enabled = canbuild;
				
				if (GUI.Button(new Rect(240, boxY + boxH - 30, 45, 25), "Build")) {
					queue.Enqueue(control.SimpleAction(
						(d) => {
							t.bench = b;
							foreach (var rc in b.buildcost) {
								man.GetComponent<Inventory>().AddToInventory(rc.r, -rc.amount);
							}
							OpenWindow(BuildBenchWindow(b));
							d.state = ActionState.Done;
						}));
				}
				GUI.enabled = true;
				boxY += boxH + 5;
			}
		};

		return window;
	}

	public GuiObjectInfo BuildBenchWindow(Bench bench) {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "LeftPane", bench.customname);
		
		window.Draw += (g) => {
			int i, o, boxH, boxY = 25;
			foreach(CraftingConversion cc in bench.craftables) {
			
				i = o = 0;
				
				int[] hasInInventory = man.GetComponent<Inventory>().GetAmounts(cc.reqs);
				boxH = Mathf.Max(cc.reqs.Length, cc.yields.Length) * 25 + 35;
				
				GUI.Box(new Rect(5, boxY, 290, boxH - 5), "");
				int index = 0;
				bool cancraft = true;
				foreach (var rc in cc.reqs) {
					SetColor(() => {return hasInInventory[index] >= rc.amount;});
					if (hasInInventory[index] < rc.amount) cancraft = false;
					GUI.Label(new Rect(10, boxY + 5 + i * 20, 150, 25), hasInInventory[index++] + "/" + rc.amount + " " + rc.r.customname);
					i++;
				}
				foreach (ResourceCount y in cc.yields) {
					GUI.Label(new Rect(225, boxY + 5 + o * 20, 70, 25), y.amount + " " + y.r.name);
					o++;
				}
				
				GUI.Label(new Rect(130, boxY + 5, 60, 25), "------>");
				
				SetColor();
				GUI.Label(new Rect(10, boxY + boxH - 35, 130, 25), cc.name);
				
				GUI.enabled = cancraft;
				if (GUI.Button(new Rect(225, boxY + boxH - 35, 60, 25), "Craft")) {
					Inventory inv = man.GetComponent<Inventory>();
					foreach (ResourceCount rc in cc.reqs) {
						inv.AddToInventory(rc.r, -rc.amount);
					}
					foreach (ResourceCount y in cc.yields) {
						inv.AddToInventory(y.r, y.amount);
					}
				}
				
				GUI.enabled = true;
				boxY += boxH;
			}
		};
		
		return window;
	}
	
	public GuiObjectInfo BuildHearthFireWindow(HearthFire h) {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "LeftPane", "Hearth Fire");
		
		window.AddChild(new GuiObjectInfo(new Rect(5, 30, 290, 25),
			(g) => {
				GUI.Label(g.rect, "Remaining Fuel: ");
				GUI.Box(new Rect(120, 30, 170, 25), "");
				GUI.Box(new Rect(120, 30, 170 * h.currentFuel / h.maxFuel, 25), "");
				GUI.Label(new Rect(120, 30, 240, 25), h.currentFuel + "/" + h.maxFuel);
			}, "FuelCount", ""));
		
		window.AddChild(new GuiObjectInfo(new Rect(5, 65, 290, 30),
			(g) => {
				GUI.Box(new Rect(5, 65, 290, 25), "");
				GUI.Box(new Rect(5, 65, (int)(290 * (60f / h.fuelPerMinute - h.timer)/(60f/h.fuelPerMinute)), 25), "");
			}, "Fueltimer", ""));
		
		Inventory inv = man.GetComponent<Inventory>();
		window.AddChild(new GuiObjectInfo(new Rect(5, 100, 290, 400), 
			(g) => {
				int i = 0;
				int[] currentResources = inv.GetAmounts(h.acceptedFuels);
				foreach (ResourceCount rc in h.acceptedFuels) {					
					GUI.Label(new Rect(5, 90 + i * 25, 290, 25), "Add " + rc.r.name + ", adds " + rc.amount + " fuel");
					if (currentResources[i] < 1 || h.currentFuel >= h.maxFuel) GUI.enabled = false; 
					if (GUI.Button(new Rect(220, 90 + i * 25, 40, 25), "Add")) {
						h.AddFuel(rc.r);
						inv.AddToInventory(rc.r, -1);
					}
					GUI.enabled = true;
					SetColor(() => {return currentResources[i] > 0;});
					GUI.Label(new Rect(270, 90 + i * 25, 25, 25), "(" + currentResources[i] + ")");
					SetColor();
					i++;
				}
			
			}, "FuelAdder", ""));
			
		window.Draw += (g) => {
			
			window.DrawAllChildren();
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
	
	private void SetColor(Func<bool> test = null) {
		if (test == null) {
			GUI.contentColor = Color.white;
		} else {
			if (test()) {
				GUI.contentColor = Color.green;	
			} else {
				GUI.contentColor = Color.red;
			}
		}
	}
	
	public List<GuiObjectInfo> GetItems() {
		return GuiItems;
	}
}
