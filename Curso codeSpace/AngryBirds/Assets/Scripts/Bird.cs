using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float maxStretch = 3;
    public LineRenderer catapultFront;
    public LineRenderer catapultBack;

    SpringJoint2D spring;
    bool clickedOn;
    Ray rayToMouse;
    Ray leftCatapultToBird;
    Vector2 prevVelocity;
    float circleRadius;
    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spring = GetComponent<SpringJoint2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        LineRendererSetUp();

        rayToMouse = new Ray(catapultBack.transform.position, Vector3.zero);
        leftCatapultToBird = new Ray(catapultFront.transform.position, Vector3.zero);

        circleRadius = GetComponent<CircleCollider2D>().radius;
    }

    // Update is called once per frame
    void Update()
    {
        if (clickedOn) Dragging();

        if (spring != null)
        {
            if (rb.bodyType != RigidbodyType2D.Kinematic && prevVelocity.magnitude > rb.velocity.magnitude)
            {
                Destroy(spring);
                rb.velocity = prevVelocity;
            }
            if (!clickedOn)
            {
                prevVelocity = rb.velocity;
            }
            LineRendererupdate();
        }
        else
        {
            Debug.Log("ehoh");
            catapultFront.enabled = false;
            catapultBack.enabled = false;
        }

    }

    private void OnMouseDown()
    {
        spring.enabled = false;
        clickedOn = true;
    }

    private void OnMouseUp()
    {
        spring.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        clickedOn = false;
    }
    
    void Dragging()
    {
        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 catapultToMouse = mouseWorldPoint - catapultBack.transform.position;

        if (catapultToMouse.magnitude > maxStretch)
        {
            rayToMouse.direction = catapultToMouse;
            mouseWorldPoint = rayToMouse.GetPoint(maxStretch);
        }
        mouseWorldPoint.z = 0;
        transform.position = mouseWorldPoint;
    }

    void LineRendererSetUp()
    {
        catapultFront.SetPosition(0, catapultFront.transform.position);
        catapultBack.SetPosition(0, catapultBack.transform.position);

        catapultFront.sortingOrder = 3;
        catapultBack.sortingOrder = 2;
    }

    void LineRendererupdate()
    {
        Vector2 catapultToBird = transform.position - catapultFront.transform.position;
        leftCatapultToBird.direction = catapultToBird;

        Vector3 point = leftCatapultToBird.GetPoint(catapultToBird.magnitude + circleRadius / 2);

        catapultFront.SetPosition(1, new Vector3(point.x, point.y, -5));
        catapultBack.SetPosition(1, new Vector3(point.x, point.y, -5));
    }
}
