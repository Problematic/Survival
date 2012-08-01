using UnityEngine;
using System.Collections.Generic;

public class Armory : WorldObject {
	
	public List<Weapon> weapons;
	public List<Armor> armors;
	public List<Potion> potions;
	
	public int GetNumItems() {
		return weapons.Count + armors.Count + potions.Count;
	}
	
}
