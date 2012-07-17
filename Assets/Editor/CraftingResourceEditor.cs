using UnityEngine;
using System.Collections;
using UnityEditor;

public static class CraftingResourceEditor {
	
	[MenuItem ("Crafting/Create Harvestable")]
	static void CreateHarvestable(){
		var o = ScriptableObject.CreateInstance<Resource>();
		AssetDatabase.CreateAsset(o,"Assets/_CraftingResources/Resources/_NewResource.asset");
	}
	
	[MenuItem ("Crafting/Create Bench")]
	static void CreateBench(){
		var o = ScriptableObject.CreateInstance<Bench>();
		AssetDatabase.CreateAsset(o,"Assets/_CraftingResources/Benches/_NewBench.asset");
	}
}

