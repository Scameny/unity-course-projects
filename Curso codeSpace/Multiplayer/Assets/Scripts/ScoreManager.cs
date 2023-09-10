using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class ScoreManager : MonoBehaviour
{
    int[] score;
    Text[] textScore;
    public static ScoreManager sm;
    private void Start()
    {
        sm = this;
        score = new int[3];
        textScore = new Text[3];
        textScore[1] = GameObject.Find("Count1").GetComponent<Text>();
        textScore[2] = GameObject.Find("Count2").GetComponent<Text>();
    }

    [PunRPC]
    public void UpdateScore(int playerNum)
    {
        score[playerNum]++;
        SyncScore();
    }

    public void SyncScore()
    {
        textScore[1].text = score[1].ToString("00");
        textScore[2].text = score[2].ToString("00");
    }
}
