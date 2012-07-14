using UnityEngine;
using System.Collections.Generic;

public class BuildableFactory : MonoBehaviour{
	
	public House house;
	
	public Table table;
	
	//private List<IConstructionMaterial> materials;
	private IConstructionMaterial material;
	
	public void ResetFactory() {
		material = null;
	}
	
	public void UseMaterial(IConstructionMaterial m) {
		material = m;
	}
	
	
	public IBuildable BuildHouse() {
		return house;
	}
	
	public IBuildable BuildTable() {
		return table;
	}
}
