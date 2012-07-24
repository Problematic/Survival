using UnityEngine;
using System.Collections.Generic;

public class Man : WorldObject {

	public float speed = 100.0f;
	public float acceleration = 1000.0f;
	protected float currentSpeed, distance, lastDistance;
	public float slowdowndistance = 0.1f;
	public float rotateSpeed = 100.0f;
	
	protected Vector3 currentFacing;
	protected Vector3 directionUnit;
	
	public Vector3 pos;
	protected Vector3 destination;
	
	public Knowledge knowledge;
	protected Status status;
	
	public CharacterController controller;
	public ActionQueue queue;
	
	// Use this for initialization
	void Start () {
		destination = transform.position;
		currentFacing = transform.up;
		
		status = new Status();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	
	public void move(Vector3 WorldLocation) {
		destination = WorldLocation;
			
		var da = new DAction();
		
		da.OnBegin += (d) => {
			directionUnit = (destination - transform.position).normalized;
			currentSpeed = 0f;
			distance = (destination - transform.position).magnitude;
		};
		
		da.OnUpdate += (d) => {
			lastDistance = distance;
			distance = (destination - transform.position).magnitude;
			currentSpeed = speed * Time.deltaTime;
			
			if (distance > lastDistance) {
			//	Debug.Log("last: " + lastDistance + " dist: " + distance);
				d.state = ActionState.Done;
				return;
			};
			
			controller.SimpleMove(directionUnit * currentSpeed);
			pos = transform.position;
			
			currentFacing = Vector3.Slerp(pos + transform.forward, destination, rotateSpeed * Time.deltaTime);
			transform.LookAt(new Vector3(currentFacing.x, pos.y, currentFacing.z));
		};
		
//da.OnEnd += (d) => {Debug.Log("Ended move");};
		
		queue.Enqueue(da);
	}

	public void Harvest(Harvestable h) {
		queue.Enqueue(new HarvestAction(GetComponent<Inventory>(), h));
	}
	
	public Status GetStatus() {
		return status;
	}
}
