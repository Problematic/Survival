using UnityEngine;
using System.Collections;
using UnityEditor;

public class Weapon : InventoryItem{	
	
	public int damageBonus = 4, speedBonus = 1;
	
	public void OnEnable() {
		description = "Damage: " + damageBonus + " Speed: " + speedBonus;
	}
	
	public bool UseItem(WorldObject target) {
		Man m = target as Man;
		if (m != null) {
			m.EquipItem(this);
		}
		return false;
	}

}