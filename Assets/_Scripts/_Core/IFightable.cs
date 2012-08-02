using UnityEngine;
using System;
using System.Collections;

public interface IFightable {
	Status GetStatus();
	T GetBest<T>(Func<T, T, T> test = null) where T : InventoryItem;
}

public class Genericenemy : WorldObject, IFightable {
	private Status status;
	public Status GetStatus() {return status;}
	public T GetBest<T>(Func<T, T, T> test = null) where T : InventoryItem {
		return default(T);
	}
	public Genericenemy() {
		status = new Status(50, 10, 10, 10);
	}
}