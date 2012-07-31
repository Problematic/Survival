using UnityEngine;
using System.Collections.Generic;

public class Man : WorldObject, IFightable {

	public float speed = 100.0f;
	public float acceleration = 1000.0f;
	public float currentSpeed, distance, lastDistance;
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
	
	public WorldObject targetObject;
	
	public bool AtFire{
		get{
			if (Static.HearthFire==null) return false;
			return Vector3.Distance(transform.position,Static.HearthFire.transform.position)<2;
		}
	}
	
	// Use this for initialization
	void Start () {
		destination = transform.position;
		currentFacing = transform.up;
		
		status = new Status();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
	}
	
	void OnControllerColliderHit(ControllerColliderHit hit) {
		var w = hit.collider.GetComponent<WorldObject>();
		if (w != null && w == targetObject) {
			destination = transform.position;
		}
	}
	
	public void MoveTo(Vector3 WorldLocation) {
		destination = WorldLocation;
			
		var da = new DAction();
		
		da.OnBegin += (d) => {
			directionUnit = (destination - transform.position).normalized;
			currentSpeed = 0f;
			distance = MathUtil.CrowFliesDistance(destination - transform.position);
//			Debug.Log ("walk>?");
			GetComponentInChildren<Animation>().Play("walk");
		};
		
		da.OnUpdate += (d) => {
			directionUnit = (destination - transform.position).normalized;
			lastDistance = distance;
			distance = MathUtil.CrowFliesDistance(destination - transform.position);
			currentSpeed = speed * Time.deltaTime;
			
			if (distance < 0.5f) {
				//Debug.Log("last: " + lastDistance + " dist: " + distance);
				d.state = ActionState.Done;
				GetComponentInChildren<Animation>().Play("idle");
				return;
			};
			
			controller.SimpleMove(directionUnit * currentSpeed);
			pos = transform.position;
			
			currentFacing = Vector3.Slerp(pos + transform.forward, destination, rotateSpeed * Time.deltaTime);
			transform.LookAt(new Vector3(currentFacing.x, pos.y, currentFacing.z));
		};
		
//da.OnEnd += (d) => {Debug.Log("Ended Moveto");};
		
		queue.Enqueue(da);
	}
	
	public void MoveTo(WorldObject target) {
			
		var da = new DAction();
		
		da.OnBegin += (d) => {
			targetObject = target;
			destination = target.transform.position;
			directionUnit = (destination - transform.position).normalized;
			currentSpeed = 0f;
			distance = MathUtil.CrowFliesDistance(destination - transform.position);
		};
		
		da.OnUpdate += (d) => {
			destination = target.transform.position;
			directionUnit = (destination - transform.position).normalized;
			lastDistance = distance;
			distance = MathUtil.CrowFliesDistance(destination - transform.position);
			currentSpeed = speed * Time.deltaTime;
			
			if (distance < 2.5f) {
			//	Debug.Log("last: " + lastDistance + " dist: " + distance);
				d.state = ActionState.Done;
				return;
			};
			
			controller.SimpleMove(directionUnit * currentSpeed);
			pos = transform.position;
			
			currentFacing = Vector3.Slerp(pos + transform.forward, destination, rotateSpeed * Time.deltaTime);
			transform.LookAt(new Vector3(currentFacing.x, pos.y, currentFacing.z));
		};
		
//da.OnEnd += (d) => {Debug.Log("Ended MoveTo");};
		
		queue.Enqueue(da);
	}
	
	public void Harvest(Harvestable h) {
		queue.Enqueue(new HarvestAction(GetComponent<Inventory>(), h));
	}
	
	public Status GetStatus() {
		return status;
	}
}
public partial class Static{
	public Man man;
	public static Man Man{
		get{
			return instance.man;
		}
	}
	
}
