using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainGen : MonoBehaviour{

	public Terrain terrain;
	public GameObject tree;
	public List<GameObject> trees;
	
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
	[ContextMenu("Spawn Trees")]
	public void SpawnStuff(){
		var g = new GameObject();
		for (int i =0;i<100;i++){
			Vector3 rand = new Vector3(Random.Range(476f,670),7, Random.Range(876f,1070f));
			if (Physics.CheckSphere(rand,5)){
				continue;
			}
			Debug.Log (rand);
			var go = (Instantiate (tree,rand-Vector3.up*2,Quaternion.identity) as GameObject);
			trees.Add(go);
			go.transform.parent=g.transform;
		}
	}
	[ContextMenu("Destroy Trees")]
	public void Destroytrees(){
		for (int i=0; i<trees.Count;i++){
			Destroy (trees[i]);
			
		}
		trees.Clear();
	}
}
