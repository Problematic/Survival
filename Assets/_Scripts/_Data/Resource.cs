using UnityEngine;
using System.Collections;
using UnityEditor;

public class Resource : ScriptableObject, IInventoryItem{	
	
	public string customName = "default";
	
	public string GetName() {
		return customName;
	}
	
	public string GetDescription() {
		return "";
	}
	
	public void OnPickUp() {}
	public void OnDrop(){}
//	public InventoryItem resource;
//	
//	public InventoryItem GetResource() {
//		return resource;
//	}
//	
//	public abstract InventoryItem Harvest();
}
