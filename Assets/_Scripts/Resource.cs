using UnityEngine;
using System.Collections;
using UnityEditor;

public partial class Resource : ScriptableObject {	
	
	public string name = "default";
	public string GetName(){
		return name;
	}
	
	
//	public InventoryItem resource;
//	
//	public InventoryItem GetResource() {
//		return resource;
//	}
//	
//	public abstract InventoryItem Harvest();
}
