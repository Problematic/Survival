using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

public class Inventory : MonoBehaviour {
	
	public ItemCount[] startingitems;
	Dictionary<InventoryItem, int> inventory = new Dictionary<InventoryItem,int>();
	
	void Start() {
		foreach(var ic in startingitems) {
			inventory.Add(ic.item, ic.amount);	
		}
	}
	
	public T GetBest<T>(Func<T, T, T> test) where T : InventoryItem {
		
		var resultList = inventory.Keys.OfType<T>();
	
		T result = default(T);
		
		if (resultList.Count() == 0) {
		} else if (test == null) {
			result = resultList.First();
		} else {	
			result = resultList.Aggregate(test);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                              
		}
		
		return result;
	}
	
	//public 
	
	public void CheckInventory() {
		inventory = inventory.Where(entry => entry.Value > 0).ToDictionary(e => e.Key, e=>e.Value);
	}
	public Dictionary<InventoryItem,int> GetInventory() {
		CheckInventory();
		return inventory;
	}

	public void AddToInventory(InventoryItem item, int amount) {
		CheckInventory();
		if (!inventory.ContainsKey(item)) {inventory.Add(item, amount); return;}
		inventory[item]+=amount;
	}
	
	public int[] GetAmounts(ItemCount[] resources) {
		int[] result = new int[resources.Length];
		int i = 0;
		foreach(ItemCount rc in resources) {
			try {
				result[i] = inventory[rc.item];
			} 
			catch (KeyNotFoundException) {
				result[i] = 0;
			}
			i++;
		}
		return result;
	}
	
	public int GetSize() {
		return inventory.Count;
	}
	
	public IEnumerable<ItemCount> FilterType<T>() where T : InventoryItem {
		return 
			from item in inventory 
				where item.Key as T != null
				select new ItemCount(item.Key, item.Value);
	}
	
}
