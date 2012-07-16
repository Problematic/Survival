using UnityEngine;
using System.Collections;

public class WorldObjectFactory : MonoBehaviour {
	
	public BuildableFactory bFactory;
	
	public void Start() {
		
		bFactory = new BuildableFactory();
	}
	
	
}

