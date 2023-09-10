using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    Transform shootPoint;
    public float shootForce;
    public float cadency;
    public GameObject fireball;
    public GameObject fireArea;
    bool canShoot = true;
    bool canShootArea = true;
    // Start is called before the first frame update
    void Awake()
    {
        shootPoint = transform.GetChild(2);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && canShoot)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                transform.LookAt(new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z));
            }
            StartCoroutine("Shoot");
        }

        if (Input.GetButtonDown("Fire1") && canShootArea)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(ray, out hit))
            {
                transform.LookAt(new Vector3(hit.transform.position.x, transform.position.y, hit.transform.position.z));
            }
            StartCoroutine("CastArea");
        }
    }

    IEnumerator Shoot()
    {
        GetComponent<PlayerMove>().enabled = false;
        canShoot = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Animator>().Play("Attack");
        yield return new WaitForSeconds(1);
        Instantiate(fireball, shootPoint.position, fireball.transform.rotation).GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);
        yield return new WaitForSeconds(cadency - 1);
        GetComponent<PlayerMove>().enabled = true;
        canShoot = true;
    }

    IEnumerator CastArea()
    {
        GetComponent<PlayerMove>().enabled = false;
        canShootArea = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Animator>().Play("Attack2H");
        yield return new WaitForSeconds(1);
        Instantiate(fireArea, shootPoint.position, shootPoint.rotation);
        yield return new WaitForSeconds(cadency - 1);
        GetComponent<PlayerMove>().enabled = true;
        canShootArea = true;
    }
}
