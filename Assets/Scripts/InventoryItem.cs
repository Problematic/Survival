using UnityEngine;
using System.Collections;

public abstract class InventoryItem {
	
	protected int quantity;
	protected string name;
	
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
	
	public string GetName() {
		return name;	
	}
	
	public int GetQuantity() {
		return quantity;
	}
	
	public string ToString() {
		return GetName() + ": " + GetQuantity();
	}
	
}
