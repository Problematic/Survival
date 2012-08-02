using UnityEngine;
using System.Collections;
using UnityEditor;

public class Weapon : InventoryItem{	
	
	public string customName = "default";
	public int damageBonus = 4, speedBonus = 1;
	
	public string GetName() {
		return customName;
	}
	
	public bool UseItem(WorldObject target) {
		Man m = target as Man;
		if (m != null) {
			m.EquipItem(this);
		}
		return false;
	}
	
	public string GetDescription() {
		return "Damage: " + damageBonus + " Speed: " + speedBonus;
	}

}