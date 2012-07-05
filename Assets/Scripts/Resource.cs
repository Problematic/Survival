using UnityEngine;
using System.Collections;

public interface Resource {	
	int Harvest();
	string GetType();
	bool IsEmpty();
}
