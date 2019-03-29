using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
public static class Global
{
    public static float[,] HeightMap;
    public static float[,] FeatureMap;
    public static int mapWidth, mapHeight;
    public static string name;

    public static int population, workforce, electricity;
    public static int food, nrtypesfood;
    public static int wood, water, stone, bricks, glass, metal;
    public static int petrol, gas, coal, biofuel, uranium;
    public static int trees = 0;
    public static float pollution;

    public static int[] buildings;
    public static bool[] isBuildingResearched;
    public static List<float> tileHeights;
    public static SortedList<float, pair> tileToSink;
}
