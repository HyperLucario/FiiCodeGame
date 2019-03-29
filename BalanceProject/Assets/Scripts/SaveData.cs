using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public float[,] HeightMap;
    public float[,] FeatureMap;
    public int mapWidth, mapHeight;
    public string name;
    public int population, workforce, electricity;
    public int food, nrtypesfood;
    public int wood, stone, bricks, glass, metal, water;
    public int petrol, gas, coal, biofuel, uranium, trees = 0;
    public float pollution = 0;
    public int[] buildings;
    public bool[] isBuildingResearched;
    public List<float> tileHeights;
    public SortedList<float, pair> tilesToSink;

    public SaveData()
    {
        HeightMap = Global.HeightMap;
        FeatureMap = Global.FeatureMap;
        mapWidth = Global.mapWidth;
        mapHeight = Global.mapHeight;
        name = Global.name;
        population = Global.population;
        workforce = Global.workforce;
        electricity = Global.electricity;
        food = Global.food;
        stone = Global.stone;
        bricks = Global.bricks;
        glass = Global.glass;
        metal = Global.metal ;
        petrol = Global.petrol;
        gas = Global.gas;
        coal = Global.coal;
        biofuel = Global.biofuel;
        uranium = Global.uranium;
        water = Global.water;
        pollution = Global.pollution;
        trees = Global.trees;
        buildings = Global.buildings;
        tilesToSink = Global.tileToSink;
        tileHeights = Global.tileHeights;
        isBuildingResearched = Global.isBuildingResearched;
    }

}
