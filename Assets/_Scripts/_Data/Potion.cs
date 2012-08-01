using UnityEngine;
using System.Collections;

public class Potion : ScriptableObject, IInventoryItem{	
	
	public string customName = "default";
	
	public string GetName() {
		return customName;
	}
	public void OnPickUp() {}
	public void OnDrop(){}
	
	public string GetDescription() {
		return	"Heals HP";
	}
}