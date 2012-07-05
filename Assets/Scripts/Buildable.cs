using UnityEngine;
using System.Collections;

public interface Buildable {
	void Build();
	bool GetGhost();
	void FollowCursor(Vector3 location);
}
