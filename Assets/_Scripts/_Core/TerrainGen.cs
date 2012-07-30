using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGen : MonoBehaviour{

	public Terrain terrain;
	public GameObject[] objs;
	
	public GameObject holder;
	
	public int numToSpawn = 200;
	
	[ContextMenu("Generate New Heightmap")]	
	public void Gen() {
		int w = terrain.terrainData.heightmapWidth, h = terrain.terrainData.heightmapHeight;
		float[,] map = terrain.terrainData.GetHeights(0, 0, w, h);
		Debug.Log(h);
		for(int x=0; x<w; x++)
		{
			for(int y=0; y<h; y++){
				map[x, y] = (Mathf.Sin(x%5+y%3)+Mathf.Cos(y%4+x%7))/800f;
			}
		}
		terrain.terrainData.SetHeights(0, 0, map);
		
	}
	[ContextMenu("Flatten World")]	
	public void Flat() {
		int w = terrain.terrainData.heightmapWidth, h = terrain.terrainData.heightmapHeight;
		float[,] map = terrain.terrainData.GetHeights(0, 0, w, h);
		Debug.Log(h);
		for(int x=0; x<w; x++)
		{
			for(int y=0; y<h; y++){
				map[x, y] = 0;
			}
		}
		terrain.terrainData.SetHeights(0, 0, map);
		
	}
	[ContextMenu("Spawn Objs")]
	public void SpawnStuff(){
		if (holder==null) holder = new GameObject("World Object Holder");
		for (int i =0;i<numToSpawn;i++){
			Vector3 rand = new Vector3(Random.Range(476f,670),7, Random.Range(876f,1070f));
			if (Physics.CheckSphere(rand,5)){
				continue;
			}
			Debug.Log (rand);
			var go = (Instantiate (objs.RandomElement(),rand-Vector3.up*7,Quaternion.identity) as GameObject);
			go.transform.parent=holder.transform;
		}
	}
	[ContextMenu("Destroy Objs")]
	public void Destroytrees(){
		Destroy(holder);
	}
}
