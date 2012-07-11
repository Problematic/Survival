using UnityEngine;
using System.Collections;

public abstract class Resource : MonoBehaviour , WorldObject {	
	
	public InventoryItem resource;
	
	public InventoryItem GetResource() {
		return	resource;
	}
	
	public abstract InventoryItem Harvest();
	
	public void Kill() {
		Destroy(gameObject);
	}
	
	public bool IsEmpty() { return resource.GetQuantity() <= 0;}
	
	public void ReceiveAction(WorldObject target) {}
}
