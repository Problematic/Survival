using UnityEngine;
using System.Collections;

public interface WorldObject {
	void ReceiveAction(WorldObject target);
}
