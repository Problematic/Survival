using UnityEngine;
using System.Collections;

public class Potion : InventoryItem{	
	
	public int healAmount = 50;
	
	void OnEnable() {
		description = "Heals " + healAmount + " HP";
	}
	
	public override void Use(WorldObject target) {
		var t = target as IFightable;
		if (t != null) {
			t.GetStatus().Heal(healAmount);
		}
	}
}