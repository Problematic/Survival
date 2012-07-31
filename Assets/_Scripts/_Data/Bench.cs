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
	public Resource item;
	public int amount;
	
	public string toString() {
		return amount + " " + item.customName;
	}
}
