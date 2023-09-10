using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{

    public int score;
    public Text scoreText;
    public Text numberEnemiesText;
    public int numEnemies = 0;
    EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GetComponent<EnemyManager>();
    }

    public void UpdateScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score.ToString();
    }

    public void UpdateNumberEnemies(bool addEnemy)
    {
        if (addEnemy) numEnemies++;
        numberEnemiesText.text = "Enemies: " + numEnemies.ToString();
    }
}
