using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerator : MonoBehaviour {

	public int mapWidth;
	public int mapHeight;
	public float noiseScale;

	public enum DrawMode {NoiseMap, ColourMap};
	public DrawMode drawMode;

	public int octaves;
	[Range(0,1)]
	public float persistance;
	public float lacunarity;

	public int seed;
	public Vector2 offset;

	public bool autoUpdate;
	public TerrainType[] regions;

	public static void GenerateMap(int mapWidth, int mapHeight, float noiseScale, int octaves, float persistance , float lacunarity, int seed, Vector2 offset) {
		float[,] noiseMap = Noise.GenerateNoiseMap (mapWidth, mapHeight, seed, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] secondMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, seed+69, noiseScale, octaves, persistance, lacunarity, offset);
        float[,] features = FeatureGenerator.buildFeatures(noiseMap, secondMap, mapWidth, mapHeight);
        SortedList<float, pair> tiles = new SortedList<float, pair>();
        List<float> h = new List<float>();

        for (int x = 0; x < mapWidth; x++){
            for (int y = 0; y < mapHeight; y++){
                if(noiseMap[x, y] < 0.4)
                {
                    pair loc = new pair();
                    //oc.height = noiseMap[x, y];
                    loc.x = x;
                    loc.y = y;
                    try
                    {
                        tiles.Add(noiseMap[x, y], loc);
                        h.Add(noiseMap[x, y]);
                    }
                    catch (ArgumentException)
                    {
                        Debug.Log("An element with Key = "+ noiseMap[x,y] +" already exists.");
                    }
                }
            }
        }
        bool[] constructResearch = new bool[24];
        constructResearch[1] = true; constructResearch[2] = true; constructResearch[14] = true; 
        Global.isBuildingResearched = constructResearch;
        Global.HeightMap = noiseMap;
        Global.mapWidth = mapWidth;
        Global.mapHeight = mapHeight;
        Global.pollution = 0;
        Global.population = 1;
        Global.water = 50;
        Global.food = 50;
        Global.tileToSink = tiles;
        h.Sort();
        Global.tileHeights = h;
        Debug.Log(tiles.Count + " " + Global.tileToSink.Count);
        MapSave.Save();

	}

	void OnValidate() {
		if (mapWidth < 1) {
			mapWidth = 1;
		}
		if (mapHeight < 1) {
			mapHeight = 1;
		}
		if (lacunarity < 1) {
			lacunarity = 1;
		}
		if (octaves < 0) {
			octaves = 0;
		}
	}

    public class comp : IComparer<TileType>
    {
        int IComparer<TileType>.Compare(TileType x, TileType y)
        {
            if (x.height > y.height) return 1;
            else return 0;
        }
    }

}

[System.Serializable]
public struct TerrainType {
	public string name;
	public float height;
	public Color colour;
}

[System.Serializable]
public struct TileType
{
    public float height;
    public int x;
    public int y;
}

[System.Serializable]
public struct pair
{
    public int x, y;
}