using UnityEngine;
using System.Collections;

public class Bench : ScriptableObject {
	public CraftingConversion[] craftables;
	public ItemCount[] buildcost;
	public string customname = "default bench";
	public string description = "--";
}

[System.Serializable]
public class CraftingConversion {
	public string name = "Generic Conversion";
	public ItemCount[] reqs;
	public ItemCount[] yields;
}
[System.Serializable]
public class ItemCount {
	public InventoryItem item;
	public int amount;
	
	public ItemCount(InventoryItem i, int a) {
		item = i;
		amount = a;
	}
	
	public string toString() {
		return amount + " " + item.customName;
	}
}
