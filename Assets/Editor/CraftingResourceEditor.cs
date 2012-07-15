using UnityEngine;
using System.Collections;
using UnityEditor;

public static class CraftingResourceEditor {
	
	[MenuItem ("Crafting/Create Harvestable")]
	static void CreateHarvestable(){
		var o = ScriptableObject.CreateInstance<Resource>();
		AssetDatabase.CreateAsset(o,"Assets/_CraftingResources/_New.asset");
	}
}

