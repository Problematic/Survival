using UnityEngine;
using System;
using System.Collections;

public interface IFightable {
	
	Inventory GetInventory();
	Status GetStatus();
	T GetBest<T>(Func<T, T, T> test = null) where T : InventoryItem;
	string GetName();
}

public class Genericenemy : WorldObject, IFightable {
	private Status status;
	private string customName = "The bad guy";
	public Status GetStatus() {return status;}
	public T GetBest<T>(Func<T, T, T> test = null) where T : InventoryItem {
		return default(T);
	}
	public string GetName() {
		return customName;
	}
	public Inventory GetInventory() {return null;}
	public Genericenemy() {
		status = new Status(50, 10, 10, 10);
	}
}