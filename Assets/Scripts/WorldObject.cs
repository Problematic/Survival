using UnityEngine;
using System.Collections;

public abstract class WorldObject : MonoBehaviour, IWorldObject {
	public virtual void ReceiveAction(IWorldObject target) {}

//	public Vector2 GetTop() {
//		return new Vector2(	
//	}
}
