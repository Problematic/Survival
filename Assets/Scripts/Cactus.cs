using UnityEngine;
using System.Collections;

public class Cactus : Resource{
	
	public Cactus() {
		resource = new CactusFood();
	}
	
	public override InventoryItem Harvest() {
		resource.Add(-5);	
		return new CactusFood(5);
	}
}

public class CactusFood : InventoryItem {	
	public CactusFood(int q = 5) {
		name = "Cactus Meat";
		quantity = q;
		autotarget = ControlData.man;
	}
	
	public override void UseAction(WorldObject target) {
		this.Add(-2);
		target.ReceiveAction(this);
	}
}
