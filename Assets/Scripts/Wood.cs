using UnityEngine;
using System.Collections;

public class Wood : InventoryItem {
	
	public string name = "Wood";
	
	public Wood() {
		quantity = 200;
	}
	
	public override string GetName() {
		return name;	
	}
	
	public override int GetQuantity() {
		return quantity;
	}
	
	public override string ToString() {
		return name + "\n" + quantity;
	}
}
