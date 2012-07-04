using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {

	public man guy;
	public KeyCode left = KeyCode.LeftArrow;
	public KeyCode right = KeyCode.RightArrow;
	public KeyCode up = KeyCode.UpArrow;
	public KeyCode down = KeyCode.DownArrow;
	
	private Vector3 directionLeft = Vector3.left;
	private Vector3 directionRight = Vector3.right;
	private Vector3 directionUp = Vector3.forward;
	private Vector3 directionDown = Vector3.back;
	
	private float speed = 100.0f;
	
	// Use this for initialization
	void Start () {
	
		guy = GetComponent<man>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(left)) {
			guy.move(directionLeft);
		} else if (Input.GetKeyDown(right)) {
			guy.move(directionRight);
		} else if (Input.GetKeyDown(up)) {
			guy.move(directionUp);	
		} else if (Input.GetKeyDown(down)) {
			guy.move(directionDown);
		}
	}
}
