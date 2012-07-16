using UnityEngine;
using System.Collections;

public class TerrainGen : MonoBehaviour{

	public Terrain terrain;

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
}
