using UnityEngine;
using System.Collections;

public interface IWorldObject {
	void ReceiveAction(IWorldObject source);

}
