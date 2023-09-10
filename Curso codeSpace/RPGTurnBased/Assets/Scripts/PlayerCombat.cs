using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerCombat : MonoBehaviour
{
    public bool auto, defenseActive;
    [Header("Stats")]
    public float health;
    public float damage, defense, speed, distanceAttack, timeAttack, inteligence;
    int turnValue;
    Animator anim;
    Slider sliderHealth, sliderTurn;
    public GameObject selectedHand;
    public List<Transform> enemy;
    public int enemySelected;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        Transform panel = GameObject.Find("Panel"+name).transform;
        panel.GetChild(0).GetComponent<Text>().text = name;
        sliderHealth = panel.GetChild(1).GetComponent<Slider>();
        sliderTurn = panel.GetChild(2).GetComponent<Slider>();
        sliderTurn.maxValue = 100;
        sliderTurn.value = 0;
        sliderHealth.maxValue = health;
        sliderHealth.value = health;
        StartCoroutine("TurnCharge");
    }

    public IEnumerator TurnCharge()
    {
        while (health > 0)
        {
            if (!CombatManager.cm.stopTime)
                turnValue++;
            sliderTurn.value = turnValue;
            if (turnValue >= 100)
            {
                defenseActive = false;
                if (auto)
                {
                    enemySelected = Random.Range(0, enemy.Count);
                    StartCoroutine("GoAttack");
                }
                else
                {
                    CombatManager.cm.SetPlayerTurn(this);
                }
                turnValue = 0;
            }
            yield return new WaitForSeconds(speed);
        }
    }

    public IEnumerator GoAttack()
    {
        Vector3 initPos = transform.position;
        anim.Play("MoveForward");
        // hacer anim correr
        while(Vector3.Distance(transform.position, enemy[enemySelected].position)> distanceAttack)
        {
            transform.position = Vector3.MoveTowards(transform.position, enemy[enemySelected].position, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }

        anim.Play("Attack");
        yield return new WaitForSeconds(timeAttack);
        enemy[enemySelected].GetComponent<PlayerCombat>().TakeDamage(damage);
        //Hacer anim attack
        anim.Play("MoveBackward");
        while (Vector3.Distance(transform.position, initPos) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, initPos, 0.1f);
            yield return new WaitForSeconds(0.01f);
        }
        anim.Play("Idle");
        CombatManager.cm.stopTime = false;
    }

    public void SetSelectedEnemy(int num)
    {
        enemySelected += num;
        if (enemySelected >= enemy.Count)
            enemySelected = 0;
        else if (enemySelected < 0)
            enemySelected = enemy.Count - 1;
        selectedHand.SetActive(true);
        selectedHand.transform.position = enemy[enemySelected].transform.position + Vector3.up * 2;
    }

    public void TakeDamage(float _damage)
    {

        float damageTaken = _damage - defense;

        if (defenseActive)
            damageTaken -= defense;

        if (damageTaken < 0)
            damageTaken = 0;


        health -= damageTaken;
        sliderHealth.value = health;

        if (health<=0)
        {
            for (int i = 0; i < enemy.Count; i++)
            {
                enemy[i].GetComponent<PlayerCombat>().enemy.Remove(transform);

            }
            if (enemy[0].GetComponent<PlayerCombat>().enemy.Count == 0)
            {
                GetComponent<CombatSpawner>().FinishCombat();
            }
        }
    }

    public void Defense()
    {
        defenseActive = true;
    }

    public void MagicHealth()
    {
        health += inteligence;
        if (health > sliderHealth.maxValue)
            health = sliderHealth.maxValue;
        sliderHealth.value = health;
    }
}
