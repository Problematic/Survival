using UnityEngine;
using System.Collections;

public class Tree : MonoBehaviour, Resource {
	
	public int resources;
	public string type = "wood";
	
	// Use this for initialization
	void Start () {
		resources = 10;
	}
	
	public int Harvest () {
		resources -= 10;
		return 10;
	}
	
	public string GetType() {
		return type;	
	}
	
	public bool IsEmpty() {
		return resources <= 0;
	}
}
