using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDisplay : MonoBehaviour
{	
	public Terrain[] Regions;
    public Feature[] Features;
	public GameObject tmModel;

	[Range(1, 100)]
	public int maxHeight;

	private Terrain findRegion(float x)
	{
		for(int i = 0; i < Regions.Length; i++){
			if(x < Regions[i].height)
				return Regions[i];
		}
		return Regions[Regions.Length-1];
	}
    
    private Feature findFeature(float x)
    {
        for (int i = 0; i < Features.Length; i++)
        {
            if (x < Features[i].height)
                return Features[i];
        }
        return Features[Features.Length - 1];
    }

	public void buildTilemap(float[,] noiseMap, float[,] featureMap, int mapWidth, int mapHeight)
	{
		Transform transform = this.gameObject.transform;
		foreach(Transform child in transform)
		{
			Destroy(child.gameObject);
		}

		for (int i = 0; i <= maxHeight+1; i++)
		{
			GameObject newTilemap = Instantiate(tmModel, transform);
			Tilemap tilemap = newTilemap.GetComponent<Tilemap>();
			TilemapRenderer tmRenderer = newTilemap.GetComponent<TilemapRenderer>();
			tmRenderer.sortingOrder = i;
		}

		for(int x = 0; x < mapWidth; x++){
			for(int y = 0; y < mapHeight; y++){
				
				Terrain local = findRegion(noiseMap[x,y]);
				Tile tile = new Tile();
				tile.sprite = local.sprite;

				int spriteHeight;
				if(noiseMap[x,y] >= 0.4) spriteHeight = 0;
				else spriteHeight = (int) Mathf.Lerp(0, maxHeight, 1.0f-noiseMap[x,y]);

				foreach(Transform child in transform)
				{
					Tilemap tilemap = child.gameObject.GetComponent<Tilemap>();
					TilemapRenderer tmRenderer = child.gameObject.GetComponent<TilemapRenderer>();
					if(spriteHeight < tmRenderer.sortingOrder) continue;
					tilemap.SetTile(new Vector3Int(x,y, 0), tile);
				}

			}
		}

        for(int x = 0; x < mapWidth; x++)
        {
            for(int y = 0; y < mapHeight; y++)
            {
                if (featureMap[x, y] == -1) continue;
                //Debug.Log(featureMap[x, y]);
                Tile featureTile = new Tile();
                featureTile.sprite = findFeature(featureMap[x, y]).sprite;
                int spriteHeight = (int) Mathf.Lerp(0, maxHeight, 1.0f - noiseMap[x, y]);
                foreach (Transform child in transform)
                {
                    Tilemap tilemap = child.gameObject.GetComponent<Tilemap>();
                    TilemapRenderer tmRenderer = child.gameObject.GetComponent<TilemapRenderer>();
                    if (spriteHeight + 1 == tmRenderer.sortingOrder)
                    {
                        Debug.Log(x + " " + y);
                        tilemap.SetTile(new Vector3Int(x, y, 1), featureTile);
                        break;
                    }
                }
            }
        }
	}
}

[System.Serializable]
public struct Terrain{
	public float height;
	public Sprite sprite;
}

[System.Serializable]
public struct Feature
{
    public float height;
    public Sprite sprite;
}