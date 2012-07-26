using UnityEngine;
using System.Collections;

public partial class Harvestable : WorldObject {
	public Resource harvestable;
	public int remainingAmount = 3;
	public int amountPerCollection = 1;
	public bool IsEmpty(){
		return remainingAmount > 0;
	}
	public Resource Harvest(){
		remainingAmount  -= amountPerCollection;
		return harvestable;
	}
	
}
