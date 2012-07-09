using UnityEngine;
using System.Collections;

public interface Resource {	
	InventoryItem GetResource();
	int Harvest();
	bool IsEmpty();
}
