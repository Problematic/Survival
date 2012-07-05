using UnityEngine;
using System.Collections;

public interface Resource {	
	int Harvest();
	string GetResourceType();
	bool IsEmpty();
}
