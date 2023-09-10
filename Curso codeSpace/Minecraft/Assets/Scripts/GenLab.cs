using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenLab : MonoBehaviour
{
    public GameObject piece;
    public int width, large;
    GameObject[,] map;
    // Start is called before the first frame update
    void Start()
    {
        map = new GameObject[width, large];
        GenMap();
        DeletePieces();
        Invoke(nameof(DeleteWalls), 0.1f);
    }


    public void GenMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < large; z++)
            {
                GameObject room = Instantiate(piece, new Vector3(x * 10, 1, z * 10), Quaternion.identity);
                map[x, z] = room;
            }
        }
    }

    public void DeletePieces()
    {
        for (int i = 0; i < 10; i++)
        {
            int x = Random.Range(0, width);
            int z = Random.Range(0, large);

            if (map[x, z] != null)
            {
                Destroy(map[x, z]);
            }
            else
                i--;
        }
    }

    public void DeleteWalls()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < large; z++)
            {
                if (map[x,z] != null) {
                    if (x < width - 1 && map[x + 1, z] != null)
                        map[x, z].transform.GetChild(0).gameObject.SetActive(false);
                    if (x > 0 && map[x - 1, z] != null)
                        map[x, z].transform.GetChild(1).gameObject.SetActive(false);
                    if (z < large - 1 && map[x, z + 1] != null)
                        map[x, z].transform.GetChild(2).gameObject.SetActive(false);
                    if (z > 0 && map[x, z - 1] != null)
                        map[x, z].transform.GetChild(3).gameObject.SetActive(false);
                }

            }
        }
    }
}
