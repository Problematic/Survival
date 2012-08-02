using UnityEngine;
using System.Collections;

public class Status {
	
	public int health, hunger, thirst, energy;
	
	public float attack = 2f, armour = 4f, speed = 4f;
	public float attackbonus = 0f, armorbonus = 0f, speedbonus = 0f;
	public float turn = 0f;
	
	public Status(int he, int hu, int th, int en) {
		health = he;
		hunger = hu;
		thirst = th;
		energy = en;
	}
	
	public Status() {
		health = 40;
		hunger = 10;
		thirst = 50;
		energy = 80;
		speed = 1f;
		attack = 3f;
		armour = 7f;
	}
	
	public void Heal(int amount) {
		health += amount;
		if (health > 100) {
			health = 100;
		}
	}
	
	public void UpdateBonuses(Armor a, Weapon w) {
		if (a != null) {
			armorbonus = a.armorBonus;
		}
		if (w != null) {
			attackbonus = w.damageBonus;
			speedbonus = w.speedBonus;
		}
	}
	
}
