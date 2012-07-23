using UnityEngine;
using System.Collections.Generic;

public class HearthFire : WorldObject {
	
	public int fuelPerMinute = 1;
	public int currentFuel = 0;
	public int maxFuel = 5;
	public Dictionary<Resource, int> fuelTypes = new Dictionary<Resource, int>();
	public ResourceCount[] acceptedFuels;
	
	public float timer = 0f;
	
	public void Start() {
		foreach (ResourceCount rc in acceptedFuels) {
			fuelTypes.Add(rc.r, rc.amount);
		}
	}
	
	public void Update() {
		if (currentFuel > 0) {
			timer += Time.deltaTime;
			if (timer > 60f / (float)fuelPerMinute) {
				timer = 0f;
				currentFuel--;
			}
		}
	}
	
	public bool AddFuel(Resource res) {
		if (canBurn(res) && currentFuel < maxFuel) {
			currentFuel += fuelTypes[res];
			return true;
		}
		return false;
	}
	
	public bool canBurn(Resource res) {
		return fuelTypes.ContainsKey(res);
	}
		
	
}
