using UnityEngine;
using System.Collections;

public class Peon : Man {
	
	public Man man;
	
	public enum states {
		moving,
		idle,
		harvesting,
		searching,
		wandering
	};
	
	public states state = states.idle;
	
	// Use this for initialization
	void Start () {
		destination = transform.position;
		currentFacing = transform.up;
	}
	
	// Update is called once per frame
	void Update () {		
		
		if (queue.IsIdle()) {
			FindClosestAct<Harvestable>();
			var h = targetObject as Harvestable;
			if (h != null) {
				MoveTo(h);
				Harvest(h);
				h = null;
				MoveTo(man);
				Give(man);
			} else Wander();
		}

		UpdateColor();
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		var w = hit.collider.GetComponent<WorldObject>();
		if (w != null && w == targetObject) {
			destination = transform.position;
		}
		Debug.Log("HIT");
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
				particleSystem.startColor = Color.yellow;
			break;
			case states.searching:
				particleSystem.startColor = Color.white;
			break;
			case states.wandering:
				particleSystem.startColor = Color.cyan;
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
				info.distance < shortestdistance) {
					Debug.Log(shortestdistance + " > " + info.distance);
					closest = info.collider.GetComponent<T>();
					shortestdistance = info.distance;
			}
			i+=0.2f;
		}
		return closest;
	}
	
	private void Give(Man target) {
		DAction da = new DAction();
		
		da.OnUpdate += (d) => {
			var inv = GetComponent<Inventory>();
			var targetinv = target.GetComponent<Inventory>();
			foreach (var i in inv.GetInventory()) {
				targetinv.AddToInventory(i.Key, i.Value);
				inv.AddToInventory(i.Key, -i.Value);
			}
			d.state = ActionState.Done;
			state = states.idle;
		};
		
		queue.Enqueue(da);
	}
	
	private void FindClosestAct<T>(float maxDistance = Mathf.Infinity) where T : WorldObject  {
		
		DAction da = new DAction();
		 
		da.OnUpdate += (d) => {
			
			float rays = 50f;
			float inc = 2.0f * Mathf.PI / rays;
			float start = Random.Range(0f, inc);
			
			state = states.searching;
			float i = start;
			
			RaycastHit info;
			T closest = null;
			float shortestdistance = maxDistance;
			
			while (i < 2.0f) {
				if (Physics.Raycast(transform.position, new Vector3(Mathf.Cos(i * Mathf.PI), 0f, Mathf.Sin(i * Mathf.PI)),
					out info, shortestdistance) && 
					info.collider.GetComponent<T>() != null &&
					info.distance < shortestdistance) {
					
						Debug.Log(shortestdistance + " > " + info.distance);
						closest = info.collider.GetComponent<T>();
						shortestdistance = info.distance;
						ChangeState(states.idle);
				}
				
				i += inc;
			}
			targetObject = closest;
			d.state = ActionState.Done;
		};
		
		queue.Enqueue(da);
	}
	
	new private void MoveTo(Vector3 location) {
		ChangeState(states.moving);
		base.MoveTo(location);
	}
	
	new private void MoveTo(WorldObject target) {
		ChangeState(states.moving);
		base.MoveTo(target);
	}
	
	new private void Harvest(Harvestable h) {
		ChangeState(states.harvesting);
		base.Harvest(h);
	}
	
	private void ChangeState(states s) {
		DAction da = new DAction();
		da.OnUpdate += (d) => {state = s; d.state = ActionState.Done;};
		queue.Enqueue(da);
	}
	
	private void Wander() {
		ChangeState(states.wandering);
		base.MoveTo(transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)));	
	}

}
