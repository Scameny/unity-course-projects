using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float speed, rotationSpeed, jumpForce;
    public LayerMask layerDestroyable, layerCreation;
    public GameObject ret, cube;
    public int damage;

    Transform cameraTransform;
    Vector3 movement;
    Rigidbody rb;
    RaycastHit hit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = transform.GetChild(0);
    }

    private void Update()
    {
        #region MOVIMIENTO
        movement = transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal");
        movement.Normalize();
        rb.velocity = movement * speed * Time.deltaTime + rb.velocity.y * Vector3.up;
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }
        #endregion
        #region Camera rotation
        cameraTransform.Rotate(Vector3.right * Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime);
        float rotXCam = cameraTransform.rotation.eulerAngles.x;
        if (rotXCam < 310 && rotXCam > 180)
            cameraTransform.localRotation = Quaternion.Euler(310, 0, 0);
        else if(rotXCam > 70 && rotXCam < 180)
            cameraTransform.localRotation = Quaternion.Euler(70, 0, 0);
        #endregion
        #region Reticula
        if (Physics.Raycast(cameraTransform.position, cameraTransform.forward, out hit, 2, layerDestroyable))
        {
            ret.GetComponent<Image>().color = Color.red;
            if (Input.GetButtonDown("Fire1"))
            {
                if (hit.collider.gameObject.GetComponent<CubeMesh>().destroyable)
                {
                    hit.collider.gameObject.GetComponent<CubeMesh>().DecreaseHealth(damage);
                }
            }
        }
        else
        {
            ret.GetComponent<Image>().color = Color.white;
        }
        #endregion
        #region crear cubo
        if (Input.GetButtonDown("Fire2"))
        {
            if (Physics.Raycast(cameraTransform.position, cameraTransform.forward,out hit, 2, layerCreation))
            {
                Vector3 diference = hit.point - hit.transform.position;
                if (diference.x == 0.5f)
                {
                    Instantiate(cube, hit.transform.position + Vector3.right, Quaternion.identity);
                } else if (diference.x == -0.5f)
                {
                    Instantiate(cube, hit.transform.position + Vector3.left, Quaternion.identity);
                }
                else if (diference.y == 0.5)
                {
                    Instantiate(cube, hit.transform.position + Vector3.up, Quaternion.identity);
                }
                else if (diference.y == -0.5)
                {
                    Instantiate(cube, hit.transform.position + Vector3.down, Quaternion.identity);
                }
                else if (diference.z == 0.5)
                {
                    Instantiate(cube, hit.transform.position + Vector3.forward, Quaternion.identity);
                }
                else if (diference.z == -0.5)
                {
                    Instantiate(cube, hit.transform.position + Vector3.back, Quaternion.identity);
                }
            }
        }
        #endregion
    }
}
