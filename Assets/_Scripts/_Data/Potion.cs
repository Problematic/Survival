using UnityEngine;
using System.Collections;

public class Potion : InventoryItem{	
	
	public string customName = "default";
	
	public int healAmount = 50;
	
	public string GetName() {
		return customName;
	}
	
	public bool UseItem(WorldObject target) {
		var t = target as IFightable;
		if (t != null) {
			t.GetStatus().Heal(healAmount);
		}
		return true;
		Destroy(this);
	}
	
	public string GetDescription() {
		return	"Heals HP";
	}
}