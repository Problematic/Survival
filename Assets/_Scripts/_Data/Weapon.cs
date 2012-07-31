using UnityEngine;
using System.Collections;
using UnityEditor;

public class Weapon : ScriptableObject, IInventoryItem{	
	
	public string customName = "default";
	
	public string GetName() {
		return customName;
	}
	public void OnPickUp() {}
	public void OnDrop(){}

}
