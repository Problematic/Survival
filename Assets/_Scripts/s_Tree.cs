using UnityEngine;
using System.Collections;

public class s_Tree : Resource {	
	// Use this for initialization
	public void Start () {
		resource = new Wood();
	}
	
	public override InventoryItem Harvest () {
		int amount = Mathf.Min(10, resource.GetQuantity());
		resource.Add(-amount);
		return new Wood(amount);
	}
}
