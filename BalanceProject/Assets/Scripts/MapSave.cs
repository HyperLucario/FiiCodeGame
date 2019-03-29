using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class MapSave
{
    public static void Save()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/save.fmm";
        Debug.Log(path);
        FileStream stream = new FileStream(path, FileMode.Create);
        SaveData data = new SaveData();
        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static void Load()
    {
        string path = Application.persistentDataPath + "/save.fmm";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            SaveData data = formatter.Deserialize(stream) as SaveData;

            Global.HeightMap = data.HeightMap;
            Global.FeatureMap = data.FeatureMap; 
            Global.mapWidth = data.mapWidth;
            Global.mapHeight = data.mapHeight;
            Global.name = data.name;
            Global.population = data.population;
            Global.workforce = data.workforce;
            Global.electricity = data.electricity;
            Global.food = data.food;
            Global.stone = data.stone;
            Global.bricks = data.bricks;
            Global.glass = data.glass;
            Global.metal = data.metal;
            Global.petrol = data.petrol;
            Global.gas = data.gas;
            Global.coal = data.coal;
            Global.biofuel = data.biofuel;
            Global.uranium = data.uranium;
            Global.pollution = data.pollution;
            Global.trees = data.trees;
            Global.water = data.water;
            Global.buildings = data.buildings;
            Global.tileToSink = data.tilesToSink;
            Global.tileHeights = data.tileHeights;
            Global.isBuildingResearched = data.isBuildingResearched;
        }
    }
 
}