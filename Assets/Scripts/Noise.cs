using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float[,] Generate(int width, int height, float scale, int octs, float persist, float lac)
    {
        float[,] map = new float[width, height];
        float maxVal = (1f - Mathf.Pow(persist, octs)) / (1f - persist);

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {

                float amp = 1;
                float freq = 1;
                float nHeight = 0;

                for (int i = 0; i < octs; i++)
                {
                    float px = x * scale * freq / width;
                    float py = y * scale * freq / height;

                    float val = Mathf.PerlinNoise(px, py) * 2 - 1;
                    nHeight += val * amp;

                    amp *= persist;
                    freq *= lac;

                }

                map[x, y] = (nHeight / maxVal + 1) / 2;
            }
        }

        return map;
    }
   
}
