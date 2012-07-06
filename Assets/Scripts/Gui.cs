using UnityEngine;
using System.Collections.Generic;

public class Gui : MonoBehaviour {

	public Man man;
	public Mouse mouse;
	public Move move;
	public Control control;
	
	
	public class GuiObjectInfo {
		public string name;
		public string text;
		public Rect rect;
		public List<GuiObjectInfo> children;
		public GuiObjectInfo(Rect r, string s, string t = "") {
			name = s;
			rect = r;
			children = new List<GuiObjectInfo>();
		}
		public List<GuiObjectInfo> GetChildren() {return children;}
		public void SetChildren(List<GuiObjectInfo> list) {children = list;}
		public void AddChild(GuiObjectInfo child) {children.Add(child);}
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
		
		GuiItems.Add(new GuiObjectInfo(new Rect(250, 10, 120, 40), "Wood Display"));
		GuiItems.Add(new GuiObjectInfo(new Rect(10, 55, 80, 20), "Build House Button"));
	}
	
	
	public GuiObjectInfo BuildInventoryWindow() {
		GuiObjectInfo window = new GuiObjectInfo(new Rect(0, 0, 300, 500), "Inventory", "Inventory");
		
		int tileWidth = 64;
		int tileHeight = 48;
		int num = 0, width = 4;
		man.GetInventory().ForEach( (i) => 
			window.AddChild(
					new GuiObjectInfo(
						new Rect(
							num % width * (tileWidth + 10),
							num/width * (tileHeight + 10),
							tileWidth,
							tileHeight
						),
						i.getName(),
						i.getName()
					)
			)
		);
		return window;
	}
	
	//public void 
	public List<GuiObjectInfo> GetItems() {
		return GuiItems;
	}
	
	// Update is called once per frame
	void OnGUI () {
		string vect = man.nextmove.ToString();
		GUI.Box(new Rect(250, 10, 120, 40), "Wood:\n" + man.wood);
		
		if (GUI.Button(new Rect(10, 55, 80, 20), "Build House")) {
			control.ClickEvent(button.Build_House);
		}

	}
}
