using UnityEngine;
using System.Collections;

public class HarvestAction : IAction {
	private Inventory inventory;
	private Harvestable harvestable;
    public ActionState state {get; set;}
	
	public HarvestAction(Inventory i, Harvestable h) {
		inventory = i;
		harvestable = h;
	}
	
	public void Begin() {}
	
	public void Update() {
		inventory.AddToInventory(harvestable.Harvest(), harvestable.amountPerCollection);
		state = ActionState.Done;
	}
	
	public void End() {}
	public void Kill() {}
	
}
