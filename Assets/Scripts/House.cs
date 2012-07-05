using UnityEngine;
using System.Collections;

public class House : MonoBehaviour, Buildable {
	
	private bool isGhost;
	
	// Use this for initialization
	void Start () {
		isGhost = true;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Build() {
		isGhost = false;
	}
	
	public bool GetGhost() {
		return isGhost;
	}
	
	public void FollowCursor(Vector3 location) {
		transform.position = location;	
	}
}
