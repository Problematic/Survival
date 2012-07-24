using UnityEngine;
using System.Collections;

public class Peon : Man {
	
	Harvestable h;
	bool b = true;
	
	private enum states {
		moving,
		idle,
		harvesting,
		searching
	};
	
	states state = states.idle;
	
	// Use this for initialization
	void Start () {
		destination = transform.position;
		currentFacing = transform.up;
	}
	
	// Update is called once per frame
	void Update () {		
		if (b) {
			h = FindClosest<Harvestable>();
			move(h.transform.position);
			Harvest(h);
			IdleAction();
		}
		UpdateColor();
	}
	
	void UpdateColor() {
		switch(state) {
			case states.idle:
				particleSystem.startColor = Color.red;
			break;
			case states.moving:
				particleSystem.startColor = Color.green;
			break;
			case states.harvesting:
				particleSystem.startColor = Color.blue;
			break;
			case states.searching:
				particleSystem.startColor = Color.magenta;
			break;
		}
	}

	private T FindClosest<T>(float maxDistance = Mathf.Infinity) where T : Component  {
		state = states.searching;
		float i = 0f;
		RaycastHit info;
		T closest = null;
		float shortestdistance = maxDistance;
		while (i < 2.0f) {
			if (Physics.Raycast(transform.position, new Vector3(Mathf.Cos(i * Mathf.PI), 0f, Mathf.Sin(i * Mathf.PI)),
				out info, maxDistance) && 
				info.collider.GetComponent<T>() != null &&
				info.collider.GetComponent<T>() != h &&
				info.distance < shortestdistance) {
					Debug.Log(shortestdistance + " > " + info.distance);
					closest = info.collider.GetComponent<T>();
					shortestdistance = info.distance;
			}
			i+=0.2f;
		}
		return closest;
	}
	
	new private void move(Vector3 location) {
		state = states.moving;
		base.move(location);
	}
	
	new private void Harvest(Harvestable h) {
		state = states.harvesting;
		base.Harvest(h);
	}
	
	private void IdleAction() {
		DAction da = new DAction();
		da.OnUpdate += (d) => {state = states.idle; d.state = ActionState.Done;};
		queue.Enqueue(da);
	}
}
