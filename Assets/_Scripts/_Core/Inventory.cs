using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

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
		CheckInventory();
		if (!inventory.ContainsKey(item)) {inventory.Add(item, amount); return;}
		inventory[item]+=amount;
	}
	
	public int[] GetAmounts(ResourceCount[] resources) {
		int[] result = new int[resources.Length];
		int i = 0;
		foreach(ResourceCount rc in resources) {
			try {
				result[i] = inventory[rc.r];
			} 
			catch (KeyNotFoundException) {
				result[i] = 0;
			}
			i++;
		}
		return result;
	}
	
}
