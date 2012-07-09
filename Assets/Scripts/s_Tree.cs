using UnityEngine;
using System.Collections;

public class s_Tree : MonoBehaviour, Resource {
	
	public InventoryItem resource;
	
	// Use this for initialization
	void Start () {
		resource = new Wood();
	}
	
	public int Harvest () {
		resource.Add(-10);
		return 10;
	}
	
	public InventoryItem GetResource() {
		return resource;
	}
	
	public bool IsEmpty() {
		return resource.GetQuantity() <= 0;
	}
}
