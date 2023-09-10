using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public bool moving, newDirection;
    public Vector3 destination;
    public List<RaycastHit2D> resultInteractable;
    Vector3 direction;
    GameObject npc;
    public bool stopPlayer;

    public LayerMask layerObstacle, layerInteractable;
    public GameObject interactableButton;
    public static Player player;
    Animator anim;
    SpriteRenderer spritRen;
    ContactFilter2D filter;


    public DataScriptable dataBase;
    public List<Item> items;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spritRen = GetComponent<SpriteRenderer>();
        resultInteractable = new List<RaycastHit2D>();
    }

    private void Start()
    {
        player = this;
        direction = Vector3.zero;
        filter = new ContactFilter2D();
        filter.layerMask = layerInteractable;
        filter.useLayerMask = true;
    }
    private void Update()
    {
        if (stopPlayer)
            return;
        if (destination != transform.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed);
            if (Vector3.Distance(transform.position, destination) < 0.01f)
            {
                transform.position = destination;
                moving = false;
                anim.SetBool("moving", moving);
            }
        }
        if (Input.GetKey(KeyCode.W) && !moving)
        {
            NewDirection(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.S) && !moving)
        {
            NewDirection(Vector3.down);
        }
        else if (Input.GetKey(KeyCode.A) && !moving)
        {
            NewDirection(Vector3.left);
        }
        else if (Input.GetKey(KeyCode.D) && !moving)
        {
            NewDirection(Vector3.right);
        } else if (Input.GetKey(KeyCode.E) && interactableButton.activeInHierarchy)
        {
            Interaction();
        }
        
    }

    void NewDirection(Vector3 direction)
    {
        interactableButton.SetActive(false);

        
        if (Physics2D.Raycast(transform.position, direction,filter, resultInteractable, 1f) > 0)
        {
            interactableButton.SetActive(true);
        }
        else if (!Physics2D.Raycast(transform.position, direction, 1.2f, layerObstacle))
        {
            if (GetComponent<CombatSpawner>() != null)
                GetComponent<CombatSpawner>().IncreaseChance();
            destination = transform.position + direction;
            moving = true;
        }
        #region Animaciones
        anim.SetBool("moving", moving);
        if (direction == Vector3.up)
        {
            anim.SetInteger("direction", 1);
            spritRen.flipX = false;
        }
        else if (direction == Vector3.down)
        {
            anim.SetInteger("direction", 0);
            spritRen.flipX = false;
        }
        else
        {
            anim.SetInteger("direction", 2);
            if (direction == Vector3.right)
                spritRen.flipX = true;
            if (direction == Vector3.left)
                spritRen.flipX = false;
        }
        #endregion
    }

    void Interaction()
    {
        if (resultInteractable[0].transform.CompareTag("npc"))
        {
            resultInteractable[0].transform.GetComponent<DialogSystem>().enabled = true;
        }
        else if (resultInteractable[0].transform.CompareTag("chest"))
        {
            resultInteractable[0].transform.GetComponent<DialogSystem>().enabled = true;
        }
    }

    public void AddItem(int id)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].id == id)
            {
                items[i].amount++;
                return;
            }
        }
        for (int j = 0; j < dataBase.items.Count; j++)
        {
            if(dataBase.items[j].id == id)
            {
                items.Add(dataBase.items[j]);
                items[items.Count -1].amount++;
                return;
            }
        }
    }
}
