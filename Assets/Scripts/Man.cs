using UnityEngine;
using System.Collections.Generic;

public class Man : MonoBehaviour, WorldObject {

	public float speed = 100.0f;
	public float acceleration = 1000.0f;
	private float currentSpeed, distance;
	public float slowdowndistance = 0.1f;
	public float rotateSpeed = 100.0f;
	
	private Vector3 currentFacing;
	
	public Terrain terrain;
	public Vector3 nextmove; 
	public Vector3 pos;
	
	public int wood = 0;
	
	private volatile List<InventoryItem> inventory;
	private Status status;
	
	private Vector3 destination;
	
	private CharacterController controller;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		destination = transform.position;
		currentFacing = transform.up;
		
		inventory = new List<InventoryItem>();
		status = new Status();
		
		Debug.Log("ADDED: " + inventory.Count);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		currentSpeed = nextmove.magnitude;
		distance = (destination - transform.position).magnitude;
		
		if (distance > slowdowndistance) {
			if (currentSpeed < speed) currentSpeed += acceleration * Time.deltaTime;
			else currentSpeed = speed;
		} else {
			currentSpeed = speed * (1 - 1 / distance / distance);	
		}
		
		controller.SimpleMove(nextmove * Time.deltaTime);
		pos = transform.position;
		
		currentFacing = Vector3.Slerp(pos + transform.forward, destination, rotateSpeed * Time.deltaTime);
		transform.LookAt(new Vector3(currentFacing.x, pos.y, currentFacing.z));
		
		Debug.DrawRay(transform.position, currentFacing, Color.magenta);
		Debug.DrawRay(transform.position, nextmove * 10f, Color.yellow);
		
		if (distance > 0.0f) {
			nextmove = (destination - transform.position).normalized * currentSpeed;
			Debug.DrawRay(transform.position, nextmove, Color.yellow);
		}
		
	}
	
	public void move(Vector3 WorldLocation) {
			destination = WorldLocation;
	}

	public void AddToInventory(InventoryItem item) {
		string name = item.GetName();
		CheckInventory();
		foreach(InventoryItem i in inventory) {
			if (i.GetName() == name) {
				i.Add(item.GetQuantity());	
				return;
			}
		}
		inventory.Add(item);
	}
	
	public void CheckInventory() {
		foreach(InventoryItem i in inventory) {
			if (i.GetQuantity() <= 0) {
				inventory.Remove(i);
			}		
		}
	}
		
	public Status GetStatus() {
		return status;
	}
	
	public List<InventoryItem> GetInventory() {
		CheckInventory();
		return inventory;
	}
	
	public void ReceiveAction(WorldObject sender) { 
		Debug.Log("a");
		if (sender as CactusFood != null) {
			Debug.Log("b");
			status.hunger = (status.hunger + 10) % 100;
		}
	}
}
