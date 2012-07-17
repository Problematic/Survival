using System;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Gui : MonoBehaviour {

	public Man man;
	public Mouse mouse;
	public Control control;
	
	public delegate void DrawGuiElement(GuiObjectInfo g);
	public delegate void OpenWindowTask(GuiObjectInfo g);
	public OpenWindowTask NewWindowTask;
	public GuiObjectInfo NewWindow;
	
	public class GuiObjectInfo {
		public string name;
		public string text;
		public Rect rect;
		public List<GuiObjectInfo> children;
		public DrawGuiElement Draw;
		public GuiObjectInfo(Rect r, string s, string t = "") {
			name = s;
			rect = r;
			text = t;
			children = new List<GuiObjectInfo>();
			Draw = (g) => GUI.Box(g.rect, g.text);
		}
		public GuiObjectInfo(Rect r, DrawGuiElement d, string s, string t = "") {
			name = s;
			rect = r;
			text = t;
			children = new List<GuiObjectInfo>();			
			Draw = d;
		}
		public void DrawAllChildren() {foreach (GuiObjectInfo child in children) {child.Draw(child);}}
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
				g.Draw(g);
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
//		bar.AddChild(new GuiObjectInfo(new Rect(x + 5, y + 5, 40, 40), 
//									(g) => {if (GUI.Button(g.rect, g.text)) {
//											NewWindowTask = ToggleWindow;
//											NewWindow = BuildInventoryWindow();
//									}},
//									"InventoryButton", "Loot")); 
		
		bar.AddChild(new GuiObjectInfo(new Rect(x + 50, y + 5, 40, 40), 
									(g) => {if (GUI.Button(g.rect, g.text)) {
											NewWindowTask = ToggleWindow;
											NewWindow = BuildCraftWindow();
									}},
									"InventoryButton", "Craft")); 
		
		bar.Draw = (g) => {
			GUI.Box(g.rect, g.text);
			g.DrawAllChildren();
		};
		return bar;
	}
	
	public GuiObjectInfo BuildGraphs() {
		int x = 700, y = 0, w = 200, h = 50;
		GuiObjectInfo box = new GuiObjectInfo(new Rect(x, y, w, h), "GraphsBox", "");
		box.AddChild(new GuiObjectInfo(new Rect(x, y , w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().health / 100.0f), g.rect.height), "");
			},
			"HealthBar", "Health")); 
		
		box.AddChild(new GuiObjectInfo(new Rect(x + w / 2 + 3, y , w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().hunger / 100.0f), g.rect.height), "");
			},
			"HungerBar", "Hunger")); 
		
		box.AddChild(new GuiObjectInfo(new Rect(x, y + 5 + (h - 5)/2, w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().energy / 100.0f), g.rect.height), "");
			},
			"EnergyBar", "Energy")); 
		
		box.AddChild(new GuiObjectInfo(new Rect(x + w / 2 + 3, y + 5 + (h - 5)/2, w / 2-2, (h - 5)/2), 
			(g) => {
				GUI.Box(g.rect, g.name);
				GUI.Box(new Rect(g.rect.x, g.rect.y, g.rect.width * ((float)man.GetStatus().thirst / 100.0f), g.rect.height), "");
			},
			"ThirstBar", "Thirst")); 
		
		box.Draw = (g) => {
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
		
		window.Draw = (g) =>
		{
			GUI.Box(g.rect,"");//new Rect(0,0,g.rect.width,g.rect.height));
						GUILayout.BeginArea(g.rect);

			int num = 1;
			man.GetComponent<Inventory>().GetInventory().Keys.ToList().ForEach( (i) => {
				Debug.Log (i.name);
				GUILayout.Label(i.name + ": "+man.GetComponent<Inventory>().GetInventory()[i]);
			});
			GUILayout.EndArea();
		};

		return window;
	}
	
	public GuiObjectInfo BuildCraftWindow() {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "LeftPane", "Crafting");	
		
		window.AddChild(new GuiObjectInfo(
			new Rect(5, 20, 290, 75),
			(g) => {
				GUI.Box(g.rect, "A TEXT\nA TEXT\nA TEXT");
			},
			"CraftingBox",
			" "));
		
		window.AddChild(new GuiObjectInfo(
			new Rect(5, 100, 40, 40),
			(g) => {
				if (GUI.Button(g.rect, "Build\nTable")) {
					control.Build(control.buildable_house);
				}},
			"housebutton",
			""));
		
		window.Draw = (g) => {
			GUI.Box(g.rect, g.text);
			g.DrawAllChildren();
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
	
	private void OpenWindow(GuiObjectInfo window) {
		int index;
		if (WindowIsOpen(window.name, out index)) return;	
		GuiItems.Add(window);
		control.AddGUIRect(window.name, window.rect);
	}
	
	private void CloseWindow(GuiObjectInfo window) {
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
