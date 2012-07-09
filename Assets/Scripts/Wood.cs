using UnityEngine;
using System.Collections;

public class Wood :InventoryItem {
	
	public string name;
	
	public Wood() {
		name = "Wood";
	}
	
	public string GetName() {
		return name;	
	}
}
