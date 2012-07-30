using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class TreeGUIRenderer : MonoBehaviour {

	public Man man;
	public Peon peon;
	public Mouse mouse;
	public Control control;
	public ActionQueue queue;
	
	public delegate void OpenWindowTask(GuiObject g);
	public OpenWindowTask NewWindowTask;
	GuiObject NewWindow;
	
	public Color green = Color.green;
	public Color red = Color.red;
	
	public MonoBehaviour HearthButton;
	//GUI Objects. These are drawn in the tree. 

	void Start () {
		BuildGUI();
		GuiItems.Add(BuildInventoryWindow());
//		GuiItems.Add ();
		AddBlocking((HearthButton as IGUIObjectBuilder).GetGUIObject());
		
	}
	void AddBlocking(GuiObject g){
		control.AddGUIRect(g.name,g.rect);
		GuiItems.Add(g);
	}
	void OnGUI () {
		NewWindowTask = null;
		NewWindow = null;
		if (GuiItems != null) {
			foreach (GuiObject g in GuiItems) {
				g.DrawElement();
			}
			
			if (NewWindowTask != null) {
				NewWindowTask(NewWindow);
			}
		}
		
	}
	public List<GuiObject> GuiItems;
		
	public void BuildGUI() {
		GuiItems = new List<GuiObject>();
		
		
		GuiItems.Add(BuildGraphs());
		OpenWindow(BuildToolBar());
	}
	

	
	public GuiObject BuildToolBar() {
		int x = 300, y = 0, w = 400, h = 50;
		GuiObject bar = new GuiObject(new Rect(x, y, w, h), "MainToolBar", "");
		
		bar.AddChild(new GuiObject(new Rect(50, 5, 40, 40), 
									(g) => {if (GUI.Button(g.rect, g.text)) {
											NewWindowTask = ToggleWindow;
											NewWindow = BuildDebugWindow();
									}},
									"RightPane", "Debug")); 

		//Time.deltaTime is passed by value in - dis iz broken.
//		bar.AddChild(new GuiObject(new Rect(95, 5, 3f /Time.deltaTime, 40), "FPS", "FPS: " + 1f /Time.deltaTime));
				
		bar.Draw += (g) => {
			GUI.Box(g.rect, g.text);
			g.DrawAllChildren();
		};
		return bar;
	}
	
	public GuiObject BuildGraphs() {
		int x = 700, y = 0, w = 200, h = 50;
		GuiObject box = new GuiObject(new Rect(x, y, w, h), "GraphsBox", "");
		box.AddChild(new GuiObject(new Rect(0, 0 , w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().health / 100.0f), g.rect.height), "");
			},
			"HealthBar", "Health")); 
		
		box.AddChild(new GuiObject(new Rect( w / 2 + 3, 0 , w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().hunger / 100.0f), g.rect.height), "");
			},
			"HungerBar", "Hunger")); 
		
		box.AddChild(new GuiObject(new Rect(0, 5 + (h - 5)/2, w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().energy / 100.0f), g.rect.height), "");
			},
			"EnergyBar", "Energy")); 
		
		box.AddChild(new GuiObject(new Rect(w / 2 + 3, 5 + (h - 5)/2, w / 2-2, (h - 5)/2), 
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
	
	public GuiObject BuildDebugWindow() {
		GuiObject window = new GuiObject(new Rect(750, 50, 270, 400), "RightPane", "Debug");
		
		window.Draw += (g) => {
			GUI.Box(g.rect, g.text);
			g.DrawAllChildren();
		};
		
		window.AddChild(new GuiObject(new Rect(5, 20, 270, 400), 
			(g) => {
				GUI.Box(new Rect(5, 85, 40, 20 + peon.queue.Size() * 20), peon.queue.Size().ToString());
				GUI.Label(g.rect, peon.state.ToString());
				if (GUI.Button(new Rect(5, 40, 40, 40), "Skip")) {
					peon.queue.CancelCurrent();
				}
			},
			"info", ""));
		return window;
	}
	
	public GuiObject BuildInventoryWindow() {
		Debug.Log("building");
		GuiObject window = new GuiObject(new Rect(Screen.width-150, Screen.height-300, 150, 300), "Inventory", "Inventory");
		
		int tileWidth = 265;
		int tileHeight = 20;
		
		window.Draw += (g) =>
		{
			GUI.Box(g.rect, g.text);
			int num = 1;
			var d = man.GetComponent<Inventory>().GetInventory();
		foreach(var i in d) {
				GUI.Box(new Rect(g.rect.x, g.rect.y+num * (tileHeight + 5), g.rect.width, tileHeight),
					i.Key.name + " -- " + i.Value);
				num++;
			};
		};

		window.Draw += (g) => {
			g.DrawAllChildren();
		};
		
		return window;
	}
	
	public GuiObject BuildBenchUpgradeWindow(Table t, Knowledge k) {
		GuiObject window = new GuiObject(new Rect(0, 0, 300, 500), "LeftPane", "Upgrade Bench");	
		
		window.Draw += (g) => {

			int boxY = 30, inc = 0;
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

	public GuiObject BuildBenchWindow(Bench bench) {
		GuiObject window = new GuiObject(new Rect(0, 0, 300, 500), "LeftPane", bench.customname);
		
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
	
	public GuiObject BuildHearthFireWindow(HearthFire h) {
		GuiObject window = new GuiObject(new Rect(0, 0, 300, 500), "LeftPane", "Hearth Fire");
		
		window.AddChild(new GuiObject(new Rect(5, 30, 290, 25),
			(g) => {
				GUI.Label(g.rect, "Remaining Fuel: ");
				GUI.Box(new Rect(120, 30, 170, 25), "");
				GUI.Box(new Rect(120, 30, 170 * h.currentFuel / h.maxFuel, 25), "");
				GUI.Label(new Rect(120, 30, 240, 25), h.currentFuel + "/" + h.maxFuel);
			}, "FuelCount", ""));
		
		window.AddChild(new GuiObject(new Rect(5, 65, 290, 30),
			(g) => {
				GUI.Box(new Rect(5, 65, 290, 25), "");
				GUI.Box(new Rect(5, 65, (int)(290 * (60f / h.fuelPerMinute - h.timer)/(60f/h.fuelPerMinute)), 25), "");
			}, "Fueltimer", ""));
		
		Inventory inv = man.GetComponent<Inventory>();
		window.AddChild(new GuiObject(new Rect(5, 100, 290, 400), 
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
			if (!Static.Man.AtFire){
				NewWindowTask=CloseWindow;
				NewWindow=window;
			}else
				window.DrawAllChildren();
		};
		return window;
	}
	
	public GuiObject BuildCombatWindow(Man friend, Man enemy) {
		GuiObject window = new GuiObject(new Rect(20, 50, 800, 500), "CentrePane", "Combat!");	
		Status friendStatus = friend.GetStatus(), enemyStatus = enemy.GetStatus();
		
		window.AddChild(new GuiObject( new Rect(20, 20, 800, 100),
			(g) => {
				GUI.Label(new Rect(10, 25, 780, 100), "Night has fallen, you are under attack!");
			}, "Text", ""));
		
		window.AddChild(new GuiObject(new Rect(20, 100, 360, 390),
			(g) => {
				GUI.Box(g.rect, g.text);
				GUI.Label(new Rect(30, 200, 200, 25), "Attack: " + friendStatus.attack);
				GUI.Label(new Rect(30, 220, 200, 25), "Armour: " + friendStatus.armour);
				GUI.Label(new Rect(30, 240, 200, 25), "Speed: " + friendStatus.speed);
			}, "FriendBox", "Friend"));
		
		window.AddChild(new GuiObject(new Rect(410, 100, 360, 390),
			(g) => {
				GUI.Box(g.rect, g.text);
				GUI.Label(new Rect(420, 200, 200, 25), "Attack: " + enemyStatus.attack);
				GUI.Label(new Rect(420, 220, 200, 25), "Armour: " + enemyStatus.armour);
				GUI.Label(new Rect(420, 240, 200, 25), "Speed: " + enemyStatus.speed);
			}, "EnemyBox", "Enemy"));
		
		return window;
	}
	
	private bool WindowIsOpen(string name, out int index) {
		int i = 0;
		foreach(GuiObject g in GuiItems) {
			index = i;
			if (g.name == name) {
				return true;
			}
			i++;
		}
		index = -1;
		return false;
	}
	
	public void OpenWindow(GuiObject window) {
		int index;
		if (WindowIsOpen(window.name, out index)) GuiItems.RemoveAt(index);	
		GuiItems.Add(window);
		control.AddGUIRect(window.name, window.rect);
	}
	
	public void CloseWindow(GuiObject window) {
		int index;
		if (!WindowIsOpen(window.name, out index)) return;
		GuiItems.RemoveAt(index);
		control.RemoveGUIRect(window.name);
	}
	
	private void ToggleWindow(GuiObject window) {
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
	
	public List<GuiObject> GetItems() {
		return GuiItems;
	}
}
[System.Serializable]
public class GuiObject {
	public delegate void DrawGuiElement(GuiObject g);

	public string name;
	public string text;
	public Rect rect;
	public List<GuiObject> children;
	public event DrawGuiElement Draw;
	public GuiObject(Rect r, string s, string t = "") {
		name = s;
		rect = r;
		text = t;
		children = new List<GuiObject>();
	}
	public GuiObject(Rect r, DrawGuiElement d, string s, string t = "") {
		name = s;
		rect = r;
		text = t;
		children = new List<GuiObject>();			
		Draw += d;
	}
	public void DrawElement() {
		if (Draw == null) {
			GUI.Box(rect, text)	;
			DrawAllChildren();
		} else {
			Draw(this);	
		}
	}
	public void DrawAllChildren() {
		GUI.BeginGroup(rect);
		foreach (GuiObject child in children) {child.Draw(child);}
		GUI.EndGroup();
	}
	public List<GuiObject> GetChildren() {return children;}
	public void SetChildren(List<GuiObject> list) {children = list;}
	public void AddChild(GuiObject child) {children.Add(child);}
	public void SetDrawAction(DrawGuiElement d) {Draw = d;}
};

public interface IGUIObjectBuilder{
	GuiObject GetGUIObject();
}

public partial class Static {
	public TreeGUIRenderer gui;
	public static TreeGUIRenderer GUI{
		get{
			return instance.gui;
		}
	}
}
