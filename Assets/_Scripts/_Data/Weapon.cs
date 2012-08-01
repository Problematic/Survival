using UnityEngine;
using System.Collections;
using UnityEditor;

public class Weapon : ScriptableObject, IInventoryItem{	
	
	public string customName = "default";
	public int damageBonus = 4, speedBonus = 1;
	
	public string GetName() {
		return customName;
	}
	public void OnPickUp() {}
	public void OnDrop(){}
	
	public string GetDescription() {
		return "Damage: " + damageBonus + " Speed: " + speedBonus;
	}

}