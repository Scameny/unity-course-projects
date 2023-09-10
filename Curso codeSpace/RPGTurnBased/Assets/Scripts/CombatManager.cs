using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombatManager : MonoBehaviour
{
    public static CombatManager cm;
    public bool stopTime;
    public PlayerCombat playerTurn;
    public GameObject buttonPanel, combatPanel;

    private void Start()
    {
        cm = this;
        combatPanel.SetActive(true);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.D) && playerTurn != null)
        {
            playerTurn.SetSelectedEnemy(1);
        } else if(Input.GetKeyDown(KeyCode.A) && playerTurn != null)
        {
            playerTurn.SetSelectedEnemy(-1);
        }
    }

    public void SetPlayerTurn(PlayerCombat player)
    {
        playerTurn = player;
        buttonPanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(buttonPanel.transform.GetChild(0).gameObject);
        stopTime = true;
    }

    public void Attack()
    {
        playerTurn.StartCoroutine("GoAttack");
        buttonPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Defense()
    {
        stopTime = false;
        playerTurn.Defense();
        buttonPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
    
    public void Magic()
    {
        stopTime = false;
        playerTurn.MagicHealth();
        buttonPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }
}
