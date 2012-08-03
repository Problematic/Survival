using UnityEngine;
using System.Collections.Generic;
using System.Linq;


public class ItemBin : WorldObject {

	public Inventory inventory;
	
	public List<ItemCount> inventoryList { 
		get {
			return inventory.FilterType<InventoryItem>().ToList<ItemCount>();
		} 
		private set {}}
	
	public int GetNumItems() {
		return inventory.GetSize();
	}
	
	public ItemCount TakeItem(InventoryItem item, int num = 1) {
		return inventory.TakeFromInventory(item, num);
	}
}
