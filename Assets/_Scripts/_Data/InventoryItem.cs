	using UnityEngine;
using System.Collections;
using UnityEditor;

public class InventoryItem : ScriptableObject{	
	public string customName = "default";
	public string description = "";
	public bool usable = false;
	
	public string GetName() {
		return customName;
	}
	
	public string GetDescription() {
		return description;
	}
	
	public bool IsUsable() {
		return usable;
	}
	
	public virtual void Use(WorldObject target){}
}
