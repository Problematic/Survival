using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {
	public Dictionary<Resource, int> inventory = new Dictionary<Resource,int>();
	
	
	
	public void CheckInventory() {
		inventory = inventory.Where(entry => entry.Value > 0).ToDictionary(e => e.Key, e=>e.Value);
	}
	public Dictionary<Resource,int> GetInventory() {
		CheckInventory();
		return inventory;
	}

	public void AddToInventory(Resource item, int amount) {
		string name = item.GetName();
		CheckInventory();
		if (!inventory.ContainsKey(item)) {inventory.Add(item, amount); return;}
		inventory[item]+=amount;
	}
	

}
