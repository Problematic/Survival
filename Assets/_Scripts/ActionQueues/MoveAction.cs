using UnityEngine;
using System.Collections;

public class MoveAction : DAction {
	public Vector3 target;
	public MoveAction (Vector3 v){
		target=v;
	}
}
