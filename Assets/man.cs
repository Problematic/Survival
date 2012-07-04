using UnityEngine;
using System.Collections;

public class man : MonoBehaviour {

	public float speed = 100.0f;
	
	public Terrain terrain;
	public Vector3 nextmove;
	
	private CharacterController controller;
	
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		controller.SimpleMove(nextmove * Time.deltaTime);
	}
	
	public void move(Vector3 direction) {
			nextmove = direction * speed;
	}
}
