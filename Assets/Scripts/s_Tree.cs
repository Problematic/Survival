using UnityEngine;
using System.Collections;

public class s_Tree : MonoBehaviour, Resource {
	
	public int resources;
	public string type = "wood";
	
	// Use this for initialization
	void Start () {
		resources = 20;
	}
	
	public int Harvest () {
		if (resources >= 10) {
			resources -= 10;
			return 10;
		} else {
			return 0;
		}
	}
	
	public string GetResourceType() {
		return type;	
	}
	
	public bool IsEmpty() {
		return resources <= 0;
	}
}
