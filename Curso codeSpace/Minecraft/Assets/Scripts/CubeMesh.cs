using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMesh : MonoBehaviour
{
    public bool destroyable;
    public int health = 10;
    public GameObject particles;

    public void DecreaseHealth(int damage)
    {
        health -= damage;
        if (health <= 0)
            Die();
    }

    private void Die()
    {
        Collider[] cubes = Physics.OverlapSphere(transform.position, 0.6f);
        Instantiate(particles, transform.position, Quaternion.identity);
        foreach (Collider cube in cubes)
        {
            if (cube.GetComponent<MeshRenderer>())
            {
                cube.GetComponent<MeshRenderer>().enabled = true;
            }
        }
        Destroy(this.gameObject);
    }
    
    public void SetDestroyable(bool destroyable)
    {
        this.destroyable = destroyable;
    }
}
