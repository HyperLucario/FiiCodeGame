using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FeatureGenerator
{
    public static float[,] buildFeatures(float[,] noiseMap, float[,] secondNoiseMap, int mapWidth, int mapHeight)
    {
        float[,] localNoiseMap = new float[mapWidth,mapHeight];
        for (int x = 0; x < mapWidth; x++)
            for (int y = 0; y < mapHeight; y++)
            {
                if (noiseMap[x, y] > 0.4)
                    localNoiseMap[x, y] = -1;
                else if (noiseMap[x, y] < 0.2)
                    localNoiseMap[x, y] = -1;
                else localNoiseMap[x, y] = secondNoiseMap[x, y];
            }
        return localNoiseMap;

    }
}
