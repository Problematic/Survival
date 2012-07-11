using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour{

	public Terrain terrain;
	
	public void Start() {
		noise();	
	}
	
	public void noise() {
		
		int w = terrain.terrainData.heightmapWidth, h = terrain.terrainData.heightmapHeight;
		float[,] map = terrain.terrainData.GetHeights(0, 0, w, h);
		float[,] newMap = new float[w, h];
		
		int x = 0, y = 0;
		foreach (float f in map) {
			newMap[x, y] = Mathf.Sin(x+y)*50f;
		}
		
		terrain.terrainData.SetHeights(0, 0, newMap);
		
	}
}
