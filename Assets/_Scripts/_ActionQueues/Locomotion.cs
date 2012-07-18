//using UnityEngine;
//using System.Collections;
//
//public class Locomotion : MonoBehaviour {
//	ActionQueue q;
//	void NewMovementCommand (Vector3 v){
//		var m = new MoveAction(v);
//		q.Enqueue(m);
//		m.OnBegin+=Face;
//	}
//	void Face (DAction d){
//		transform.LookAt((d as MoveAction).target);
//	}
//	void Move (MoveAction d){
////		transform.Translate...
//	}
//}
