using UnityEngine;
using UnityEngine.UI;
public class CarTurbo : MonoBehaviour
{
    public float turboSpeed;
    public float factorSpeed;
    public Image turboImage;


    CarMovement carMovement;
    float normalSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        carMovement = GetComponent<CarMovement>();
    }

    void Start()
    {
        normalSpeed = carMovement.speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift)) Turbo();
        else
        {
            carMovement.speed = normalSpeed;
            turboImage.fillAmount += Time.deltaTime * factorSpeed;
        }
    }

    void Turbo()
    {
        if (turboImage.fillAmount == 0)
        {
            carMovement.speed = normalSpeed;
            return;
        }
        carMovement.speed = turboSpeed;
        turboImage.fillAmount -= Time.deltaTime * factorSpeed;
    }
}
