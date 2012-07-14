using UnityEngine;
using System.Collections;

public class Rock : Resource {
	
	public void Start() {
		resource = new Stone();		
	}
	
	public override InventoryItem Harvest () {
		int amount = Mathf.Min(10, resource.GetQuantity());
		resource.Add(-amount);
		return new Stone(amount);
	}
}

public class Stone : InventoryItem {
	public Stone(int q = 30) {
		name = "Rock Meat";
		quantity = q;
	}
}
