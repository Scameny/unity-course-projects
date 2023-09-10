using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSpawner : MonoBehaviour
{
    public GameObject combatObject;
    public int chance;

    public void IncreaseChance()
    {
        chance++;
        if (chance > 20 && Random.Range(0, 100) < chance)
        {
            combatObject.SetActive(true);
            chance = 0;
            GetComponent<Player>().enabled = false;
            Camera.main.GetComponent<CameraFollow>().combat = true;
        }
    }

    public void FinishCombat()
    {
        combatObject.SetActive(false);
        GetComponent<Player>().enabled = true;
        Camera.main.GetComponent<CameraFollow>().combat = false;
    }
}
