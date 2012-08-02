using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Armory : ItemBin {
	
	public List<ItemCount> weapons {
		get {return inventory.FilterType<Weapon>().ToList<ItemCount>();} 
		private set{}
	}
	public List<ItemCount> armors {
		get {return inventory.FilterType<Armor>().ToList<ItemCount>();}
		private set{}
	}
	public List<ItemCount> potions {
		get {return inventory.FilterType<Potion>().ToList<ItemCount>();}
		private set{}
	}
	
	
	new public int GetNumItems() {
		return weapons.Count + armors.Count + potions.Count;
	}
	
}
