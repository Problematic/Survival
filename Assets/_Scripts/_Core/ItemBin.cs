using UnityEngine;
using System.Collections;

public class ItemBin : WorldObject {

	public Inventory inventory;
	public int GetNumItems() {
		return inventory.GetSize();
	}
	
	public InventoryItem TakeItem(InventoryItem item, int num = 1) {
		inventory.AddToInventory(item, -num);
		return item;
	}
}
