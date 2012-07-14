using UnityEngine;
using System.Collections;

public abstract class InventoryItem : IWorldObject{
	
	protected int quantity;
	protected string name;
	protected WorldObject autotarget;
	protected Control control;
	
	//public InventoryItem Copy();
	
	public InventoryItem() {
		quantity = 10;
		autotarget = null;
	}
	
	public InventoryItem(int i) {
		quantity = i;
	}
	
	public InventoryItem(int i , string s) {
		quantity = i;
		name = s;
	}
	
	public virtual void UseAction(IWorldObject target) {}
	public virtual void UseAction() {}

	public void ReceiveAction(IWorldObject target) {}
	
	public WorldObject GetTarget() {
		return autotarget;
	}
	
	public void Add(int amount) {
		quantity += amount;
	}
	
	public string GetName() {
		return name;	
	}
	
	public int GetQuantity() {
		return quantity;
	}
	
	public override string ToString() {
		return GetName() + ": " + GetQuantity();
	}
	
}
