using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coroutines : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("MyFirstCoroutine");
    }

    // Update is called once per frame
    IEnumerator MyFirstCoroutine()
    {
        Debug.Log("Hola que tal");
        yield return new WaitForSeconds(3);
        Debug.Log("Soy un Jedi");
    }
}
