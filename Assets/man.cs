using UnityEngine;
using System.Collections;

public class Man : MonoBehaviour {

	public float speed = 100.0f;
	
	public Terrain terrain;
	public Vector3 nextmove; 
	public Vector3 pos;
	
	public int wood = 0;
	private Vector3 destination;
	
	private CharacterController controller;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		destination = Vector3.zero;
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		controller.SimpleMove(nextmove * Time.deltaTime);
		
		pos = transform.position;
		
		if (destination.magnitude > 0.0f) {
			nextmove = (destination - transform.position).normalized * speed;
			Debug.DrawRay(transform.position, nextmove, Color.yellow);
		}
	}
	
	public void move(Vector3 WorldLocation) {
			destination = WorldLocation;
	}
	
	public void AddResource(string type, int amount) {
		wood += amount;
	}
}
