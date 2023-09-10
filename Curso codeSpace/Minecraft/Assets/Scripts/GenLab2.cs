using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenLab2 : MonoBehaviour
{
    public GameObject piece;

    public Transform mapParent;
    public List<Transform> map;
    public GameObject player;
    public int limit;
    // Start is called before the first frame update
    void Start()
    {
        map = new List<Transform>();
        StartCoroutine(GenerateMap2(0, 0));
    }


    public IEnumerator GenerateMap(int x, int z)
    {
        limit--;
        Transform newPiece = Instantiate(piece, new Vector3(x,0,z), Quaternion.identity).transform;
        map.Add(newPiece);
        yield return new WaitForSeconds(0.1f);
        if (limit > 0)
        {
            for (int i = 0; i < 20; i++)
            {
                int random = Random.Range(0, 100);
                if (random < 25 && !Physics.Raycast(newPiece.position, Vector3.right, 10))
                {
                    StartCoroutine(GenerateMap(x + 10, z));
                    break;
                } else if(random < 50 && !Physics.Raycast(newPiece.position, Vector3.left, 10))
                {
                    StartCoroutine(GenerateMap(x - 10, z));
                    break;
                }
                else if(random < 75 && !Physics.Raycast(newPiece.position, Vector3.forward, 10))
                {
                    StartCoroutine(GenerateMap(x , z + 10));
                    break;
                }
                else if(!Physics.Raycast(newPiece.position, Vector3.back, 10))
                {
                    StartCoroutine(GenerateMap(x, z - 10));
                    break;
                }
            }
        }
    }

    public IEnumerator GenerateMap2(int x, int z)
    {
        if (Physics.OverlapSphere(new Vector3(x, 0, z), 1).Length == 0)
        {
            limit--;
            Transform newPiece = Instantiate(piece, new Vector3(x, 0, z), Quaternion.identity, mapParent).transform;
            map.Add(newPiece);
            if (x==0 && z == 0)
            {
                Instantiate(player, new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
        yield return new WaitForSeconds(0.1f);
        if (limit > 0)
        {
            int random = Random.Range(0, 100);
            if (random < 25)
            {
                StartCoroutine(GenerateMap2(x + 10, z));
            }
            else if (random < 50)
            {
                StartCoroutine(GenerateMap2(x - 10, z));
            }
            else if (random < 75)
            {
                StartCoroutine(GenerateMap2(x, z + 10));
            }
            else
            {
                StartCoroutine(GenerateMap2(x, z - 10));
            }
        }
        else
        {
            DestroyWalls();
        }

    }

    public void DestroyWalls()
    {
        foreach (Transform item in map) 
        {
            if (Physics.Raycast(item.position, Vector3.right, 10))
                item.GetChild(0).gameObject.SetActive(false);
            if (Physics.Raycast(item.position, Vector3.left, 10))
                item.GetChild(1).gameObject.SetActive(false);
            if (Physics.Raycast(item.position, Vector3.forward, 10))
                item.GetChild(2).gameObject.SetActive(false);
            if (Physics.Raycast(item.position, Vector3.back, 10))
                item.GetChild(3).gameObject.SetActive(false);
        }
    }
}
