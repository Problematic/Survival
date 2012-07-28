using UnityEngine;
using System.Collections;

public class HearthButton : MonoBehaviour, IGUIObjectBuilder {
	public Texture2D hearthIcon;
	public Texture2D hearthIconUp;
	public Texture2D hearthIconDown;
	public GuiObject GetGUIObject(){
			GuiObject bar = new GuiObject(new Rect(0, Screen.height-hearthIcon.width, hearthIcon.width, hearthIcon.height), "Hearth Button", "");
	
			if (Static.HearthFire==null){
				return DownBar(bar);
			}else
			if (Vector3.Distance(Static.Man.transform.position,Static.HearthFire.transform.position)<0.5){
				return UpBar(bar);
			}else{
				return TeleportBar(bar);
			}
		
	}
	GuiObject UpBar(GuiObject bar){
		bar.Draw += (g) =>
		{
			GUI.Button(g.rect,hearthIconUp);
		};
		return bar;
	}
	GuiObject TeleportBar(GuiObject bar){
		bar.Draw += (g) =>
		{ 
			if (GUI.Button(g.rect,hearthIcon)){
				Static.Man.transform.position=Static.HearthFire.transform.position+Vector3.up;
				Static.Man.MoveTo(Static.HearthFire.transform.position);
			}
		};
		return bar;
	}

	GuiObject DownBar(GuiObject bar){
		bar.Draw += (g) =>
		{
			GUI.Box(g.rect,hearthIconDown);
		};
		return bar;
	}
}
