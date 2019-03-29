using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FeatureGenerator
{
    public static float[,] buildFeatures(float[,] noiseMap, float[,] secondNoiseMap, int mapWidth, int mapHeight)
    {
        System.Random rand = new System.Random();
        float[,] localNoiseMap = new float[mapWidth,mapHeight];
        for (int x = 0; x < mapWidth; x++)
            for (int y = 0; y < mapHeight; y++)
            {
                if (noiseMap[x, y] >= 0.3)
                    if (noiseMap[x, y] >= 0.5 && rand.Next(1, 1000) < 2)
                        localNoiseMap[x, y] = 2;
                    else localNoiseMap[x, y] = -1;
                else if (noiseMap[x, y] <= 0.2 && rand.Next(1, 100) < 2)
                    localNoiseMap[x, y] = 1;
                else if (secondNoiseMap[x, y] <= 0.45)
                    localNoiseMap[x, y] = 0;
                else localNoiseMap[x, y] = -1;
            }
        int[] unVector = new int[30];
        for (int i = 0; i <= 29; i++)
            unVector[i] = 0;
        Global.buildings = unVector;
        Global.FeatureMap = localNoiseMap;
        return localNoiseMap;

    }
}
