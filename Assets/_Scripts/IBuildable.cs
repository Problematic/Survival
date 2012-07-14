using UnityEngine;
using System.Collections;

public interface IBuildable {
	void Build();
	bool GetGhost();
	void FollowCursor(Vector3 location);
}
