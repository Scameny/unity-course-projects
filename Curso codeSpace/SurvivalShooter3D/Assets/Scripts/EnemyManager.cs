using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyManager : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject[] prefabs;
    public PlayerHealth playerHealth;
    public static EnemyManager enemyManager { get; private set; }

    GameManager gameManager;
    int enemies = 0;
    int enemiesAlive = 0;
    private void Awake()
    {
        gameManager = GetComponent<GameManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = this;
        InvokeRepeating("Spawn", gameManager.spawnTimerPerWave[gameManager.wave - 1], gameManager.spawnTimerPerWave[gameManager.wave - 1]);
    }

    void Spawn()
    {
        if (playerHealth.currentHealth <= 0) return;
        if (gameManager.enemiesPerWave[gameManager.wave - 1] <= enemies) CancelInvoke("Spawn");
        Instantiate(prefabs[Random.Range(0, prefabs.Length - 1)], spawnPoints[Random.Range(0, spawnPoints.Length - 1)]);
        enemies += 1;
        enemiesAlive += 1;
    }
    public void EnemyDeath()
    {
        enemiesAlive -= 1;
        if (enemiesAlive == 0 && enemies == gameManager.enemiesPerWave[gameManager.wave - 1])
        {
            if (gameManager.wave <= gameManager.numberOfWaves)
            {
                enemies = 0;
                gameManager.wave += 1;
                StartCoroutine(StartNextWave());
            }
        }
    }

    IEnumerator StartNextWave()
    {
        gameManager.waveText.GetComponent<Text>().text = "Wave " + gameManager.wave;
        gameManager.waveText.SetActive(true);
        yield return new WaitForSeconds(1);
        gameManager.waveText.SetActive(false);
        InvokeRepeating("Spawn", gameManager.spawnTimerPerWave[gameManager.wave - 1], gameManager.spawnTimerPerWave[gameManager.wave - 1]);
    }
}
