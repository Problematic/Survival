using UnityEngine;
using System.Collections;

public class Table : MonoBehaviour, IBuildable, IWorldObject {
	
	private bool isGhost = false;
	public Color ghost = new Color(0.3f, 1.0f, 0.3f, 0.1f), 
				 placed = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public string shaderType = "_Color";
	
	public Table() {
		
	}
	
	public void ReceiveAction(IWorldObject source) {
		
	}
	
	public void Build() {
		isGhost = false;
		collider.enabled = true;
		renderer.material.SetColor(shaderType, placed);
	}
	
	public bool GetGhost() {
		return isGhost;
	}
	
	public void FollowCursor(Vector3 location) {
		transform.position = location;	
	}
	
	
}
