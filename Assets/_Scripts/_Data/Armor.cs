using UnityEngine;
using System.Collections;

public class Armor : InventoryItem{	
	
	new public string customName = "default";
	public int armorBonus = 2;
	
	public string GetName() {
		return customName;
	}
	public void OnPickUp() {}
	public void OnDrop(){}

	public string GetDescription() {
		return "Armor: " + armorBonus;
	}
	
	public bool UseItem(WorldObject target) {
		Man m = target as Man;
		if (m != null) {
			m.EquipItem(this);
		}
		return false;
	}
}
