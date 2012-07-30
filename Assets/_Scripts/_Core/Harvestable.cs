using UnityEngine;
using System.Collections;

#if UNITY_EDITOR
[ExecuteInEditMode]
public partial class Harvestable : WorldObject{
	void Update(){
		SetModel();
	}
}
#endif

public partial class Harvestable : WorldObject {
	public GameObject Full;
	public GameObject Empty;
	public Resource harvestable;
	public int remainingAmount = 3;
	public int amountPerCollection = 1;
	public bool IsEmpty(){
		return remainingAmount > 0;
	}
	public Resource Harvest(){
		remainingAmount  -= amountPerCollection;
		SetModel();
		return harvestable;
	}
	void Awake(){
		SetModel();
	}
	

	
	void SetModel(){
		if (!Empty || !Full) return;
		if (remainingAmount<=0){
			Full.SetActiveRecursively(false);
			Empty.SetActiveRecursively(true);
		}else{
			Full.SetActiveRecursively(true);
			Empty.SetActiveRecursively(false);
		}
	}
}
