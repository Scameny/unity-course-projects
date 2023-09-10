using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSaveData : MonoBehaviour
{
    ScoreManager scoreManager;
    // Start is called before the first frame update
    void Awake()
    {
        scoreManager = GetComponent<ScoreManager>();
        LoadData();
    }

    // Update is called once per frame
    public void SaveData()
    {
        PlayerPrefs.SetInt("numEnemies", scoreManager.numEnemies);
    }

    public void LoadData()
    {
        scoreManager.numEnemies = PlayerPrefs.GetInt("numEnemies");
        scoreManager.UpdateNumberEnemies(false);
    }
}
