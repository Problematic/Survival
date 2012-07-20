using UnityEngine;
using System.Collections;

public class Bench : ScriptableObject {
	public CraftingConversion[] craftables;
	public ResourceCount[] buildcost;
	public string customname = "default bench";
	public string description = "--";
}

[System.Serializable]
public class CraftingConversion {
	public string name = "Generic Conversion";
	public ResourceCount[] reqs;
	public ResourceCount[] yields;
}
[System.Serializable]
public class ResourceCount {
	public Resource r;
	public int amount;
	
	public string toString() {
		return amount + " " + r.GetName();
	}
}
