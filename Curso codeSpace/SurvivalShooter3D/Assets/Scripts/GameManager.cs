using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject waveText;
    public int numberOfWaves;
    public int[] enemiesPerWave;
    public float[] spawnTimerPerWave;
    EnemyManager enemyManager;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    public int wave = 1;


    private void Awake()
    {
        enemyManager = GetComponent<EnemyManager>();
        playerMovement = player.GetComponent<PlayerMovement>();
        playerShooting = player.GetComponentInChildren<PlayerShooting>();
    }
    // Start is called before the first frame update
    IEnumerator Start()
    {
        waveText.SetActive(true);
        yield return new WaitForSeconds(1);
        waveText.SetActive(false);
        enemyManager.enabled = true;
        playerMovement.enabled = true;
        playerShooting.enabled = true;
    }
}
