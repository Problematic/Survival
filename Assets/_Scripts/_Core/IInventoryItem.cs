using UnityEngine;
using System.Collections;

public interface IInventoryItem {
	
	string GetName();
	string GetDescription();
//	int GetAmount();
	
	void OnPickUp();
	void OnDrop();
	
}
