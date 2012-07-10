using UnityEngine;
using System.Collections;

public class Wood : InventoryItem {
	public Wood(int q = 30) {
		name = "Tree Meat";
		quantity = q;
	}
}

public class HouseWood : InventoryItem {
	public HouseWood(int q = 300) {
		name = "House Meat";
		quantity = q;
	}
}
