using UnityEngine;
using System.Collections;

public partial class Static : MonoBehaviour {
	static Static instance;
	public void Awake(){
		instance=this;	
	}
}
