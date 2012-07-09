using System;
using UnityEngine;
using System.Collections.Generic;

public class Gui : MonoBehaviour {

	public Man man;
	public Mouse mouse;
	public Move move;
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
		public List<GuiObjectInfo> GetChildren() {return children;}
		public void SetChildren(List<GuiObjectInfo> list) {children = list;}
		public void AddChild(GuiObjectInfo child) {children.Add(child);}
		public void SetDrawAction(DrawGuiElement d) {Draw = d;}
	};
	
	public List<GuiObjectInfo> GuiItems;
	
	public enum button {
		Build_House	
	};
	
	// Use this for initialization
	void Start () {
		BuildGUI();
	}
	
	public void BuildGUI() {
		GuiItems = new List<GuiObjectInfo>();
		
		GuiItems.Add(new GuiObjectInfo(new Rect(0, 10, 120, 40), "Wood", "Wood Display"));
		GuiItems.Add(new GuiObjectInfo(new Rect(10, 55, 80, 20), 
			(GuiObjectInfo g) => {if (GUI.Button(g.rect, g.text)) {control.ClickEvent(button.Build_House);}},
			"Maek House", 
			"Build House"));
		
		OpenWindow(BuildToolBar());
	}
	
	public GuiObjectInfo BuildToolBar() {
		int x = 300, y = 0, w = 400, h = 50;
		GuiObjectInfo bar = new GuiObjectInfo(new Rect(x, y, w, h), "MainToolBar", "");
		bar.AddChild(new GuiObjectInfo(new Rect(x + 5, y + 5, 40, 40), 
									(g) => {if (GUI.Button(g.rect, g.text)) {
											NewWindowTask = ToggleWindow;
											NewWindow = BuildInventoryWindow();
									}},
									"InventoryButton", "inv")); 
		
		bar.Draw = (g) => {
			GUI.Box(g.rect, g.text);
			foreach (GuiObjectInfo child in g.GetChildren()) {
				child.Draw(child);	
			}
		};
		return bar;
	}
	
	public GuiObjectInfo BuildInventoryWindow() {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "Inventory", "Inventory");
		
		int tileWidth = 64;
		int tileHeight = 48;
		int width = 4;
		
		window.Draw = (g) =>
		{
			GUI.Box(g.rect, g.text);
			int num = 0;
			man.GetInventory().ForEach( (i) => {
				if (GUI.Button(new Rect(
								num % width * (tileWidth + 10),
								num/width * (tileHeight + 10),
								tileWidth,
								tileHeight
							),
							i.ToString())) {
					control.SpawnTree();	
				}
				num++;
				});
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
	
	// Update is called once per frame
	void OnGUI () {
		NewWindowTask = null;
		NewWindow = null;
//		string vect = man.nextmove.ToString();
		if (GuiItems != null) {
			foreach (GuiObjectInfo g in GuiItems) {
				g.Draw(g);
			}
			
			if (NewWindowTask != null) {
				NewWindowTask(NewWindow);
			}
		}
	}
}
