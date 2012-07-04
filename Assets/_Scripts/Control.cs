using UnityEngine;
using System.Collections;

public class Control : MonoBehaviour {
	
	public Man man;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Harvest(Resource res) {
		int amount = res.Harvest();
		string type = res.GetType();
		
		man.AddResource(type, amount);
		
		if (res.IsEmpty()) {
		//	Destroy(res);
		}
	}
	
	public void MoveMan(Vector3 WorldLocation) {
		man.move(WorldLocation);
	}		 
}
