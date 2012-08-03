using UnityEngine;
using System.Collections;

public class ItemBin : WorldObject {

	public Inventory inventory;
	public int GetNumItems() {
		return inventory.GetSize();
	}
	
	public ItemCount TakeItem(InventoryItem item, int num = 1) {
		return inventory.TakeFromInventory(item, num);
	}
}
