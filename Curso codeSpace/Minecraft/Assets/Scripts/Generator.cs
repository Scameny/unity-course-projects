using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    public int width, large, seed,heigth, rockLayer;
    public float detail;
    public GameObject cubeDirt, cubeRock, cubeGrass, player;


    private void Start()
    {
        GenerateMap();
    }
    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < large; z++)
            {
                int yMax = (int) (Mathf.PerlinNoise((x + seed) * detail, (z + seed) * detail) * heigth);  
                for (int y = 0; y <= yMax; y++)
                {
                    GameObject newCube;
                    if (y < yMax)
                    {
                        if (y < rockLayer)
                        {
                            newCube = Instantiate(cubeRock, new Vector3(x, y, z), Quaternion.identity);
                        }
                        else
                        {
                            newCube = Instantiate(cubeDirt, new Vector3(x, y, z), Quaternion.identity);
                        }
                        newCube.GetComponent<MeshRenderer>().enabled = false;

                    }
                    else
                    {
                        newCube = Instantiate(cubeGrass, new Vector3(x, y, z), Quaternion.identity);
                    }
                }
            }
        }
        Instantiate(player, new Vector3(width / 2, heigth, large / 2), Quaternion.identity);
    }
}
