	using UnityEngine;
using System.Collections;
using UnityEditor;

public class InventoryItem : ScriptableObject{	
	public string customName = "default";
	public string description = "";
	
	public string GetName() {
		return customName;
	}
	
	public bool UseItem(WorldObject target) {
		return false;	
	}
	
	public string GetDescription() {
		return description;
	}
}
