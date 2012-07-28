using UnityEngine;
using System.Collections;

public class HearthButton : MonoBehaviour, IGUIObjectBuilder {
	public GuiObject GetGUIObject(){
		GuiObject bar = new GuiObject(new Rect(0, Screen.height-50, 50, 50), "Hearth Button", "");
		
		bar.Draw += (g) =>
		{
			GUI.Box(g.rect,"asdfadsf");
		};
		return bar;
	}
}
