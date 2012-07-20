using UnityEngine;
using System.Collections;
using UnityEditor;

public partial class Resource : ScriptableObject {	
	
	public string customname = "default";
	public string GetName(){
		return customname;
	}
	
	
//	public InventoryItem resource;
//	
//	public InventoryItem GetResource() {
//		return resource;
//	}
//	
//	public abstract InventoryItem Harvest();
}
