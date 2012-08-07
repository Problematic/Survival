using UnityEngine;
using System.Collections;

public class Armor : InventoryItem{	
	
	public int armorBonus = 2;

	public void OnPickUp() {}
	public void OnDrop(){}
	
	void OnEnable() {
		description = "Armor: " + armorBonus;
		usable = true;
	}
	
	public override void Use(WorldObject target) {
		Man m = target as Man;
		if (m != null) {
			m.EquipItem(this);
		}
	}
}
