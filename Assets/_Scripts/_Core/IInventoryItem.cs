using UnityEngine;
using System.Collections;

public interface IInventoryItem {
	
	string GetName();
//	int GetAmount();
	
	void OnPickUp();
	void OnDrop();
	
}
