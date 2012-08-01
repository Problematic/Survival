using UnityEngine;
using System.Collections;

public class Armor : ScriptableObject, IInventoryItem{	
	
	public string customName = "default";
	public int armorBonus = 2;
	
	public string GetName() {
		return customName;
	}
	public void OnPickUp() {}
	public void OnDrop(){}

	public string GetDescription() {
		return "Armor: " + armorBonus;
	}
}
