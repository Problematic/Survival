using UnityEngine;
using System.Collections;

public class House : MonoBehaviour, Buildable {
	
	private bool isGhost;
	
	public Color ghost = new Color(0.3f, 1.0f, 0.3f, 0.1f), 
				 placed = new Color(1.0f, 1.0f, 1.0f, 1.0f);
	public string shaderType = "_Color";
	
	// Use this for initialization
	void Start () {
		isGhost = true;
		collider.enabled = false;
		renderer.material.SetColor(shaderType, ghost);
	}
	
	// Update is called once per frame
	void Update () {
		
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
