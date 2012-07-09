using UnityEngine;
using System.Collections;

public abstract class InventoryItem {
	
	protected int quantity;
	 
	public abstract string GetName();
	public abstract int GetQuantity();
	//public InventoryItem Copy();
	
	public InventoryItem() {
		quantity = 10;
	}
	
	public InventoryItem(int i) {
		quantity = i;
	}

	public void Add(int amount) {
		quantity += amount;
	}
	
	string ToString() {
		return GetName() + ": " + GetQuantity();
	}
	
}
