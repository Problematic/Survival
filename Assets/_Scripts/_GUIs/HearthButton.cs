using UnityEngine;
using System.Collections;

public class HearthButton : MonoBehaviour, IGUIObjectBuilder {
	public Texture2D hearthIcon;
	public Texture2D hearthIconUp;
	public Texture2D hearthIconDown;
	public GuiObject GetGUIObject(){
		GuiObject bar = new GuiObject(new Rect(0, Screen.height-hearthIcon.width, hearthIcon.width, hearthIcon.height), "Hearth Button", "");
		Debug.Log (Vector3.Distance(Static.Man.transform.position,Static.HearthFire.transform.position));
			bar.Draw += (g) =>
			{
			if (Static.HearthFire==null){
				if (GUI.Button(g.rect,hearthIconDown)){
					Instantiate(Static.HearthFirePrefab, Static.Man.transform.position+Static.Man.transform.forward.normalized, Quaternion.identity);
				}
			}else
			if (Vector3.Distance(Static.Man.transform.position,Static.HearthFire.transform.position)<2){
				if(GUI.Button(g.rect,hearthIconUp)){
					Destroy (Static.HearthFire.gameObject);
				}
			}else{
				if (GUI.Button(g.rect,hearthIcon)){
					Static.Man.transform.position=Static.HearthFire.transform.position+Vector3.up;
					Static.Man.MoveTo(Static.HearthFire.transform.position);
				}
			}
		};
		return bar;
	}
}
