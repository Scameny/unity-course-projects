using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    public GameObject button;
    public GameObject[,] matrix;
    public int width, height, bombs;
    public GameObject gameOverPanel;


    public static GameManager gm;
    private void Start()
    {
        gm = this;
        matrix = new GameObject[20, 10];
        Cursor.lockState = CursorLockMode.Locked;
        StartCoroutine(nameof(CreateMap));
    }

    public IEnumerator CreateMap()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                GameObject newButton = Instantiate(button, transform);
                newButton.GetComponent<ButtonElement>().SetValues(j, i);
                matrix[j, i] = newButton;
                yield return new WaitForSeconds(0.01f);
            }
        }
        CreateBombs();
    }

    public void CreateBombs()
    {
        for (int i = 0; i < bombs; i++)
        {
            int randomX = Random.Range(0, width);
            int randomY = Random.Range(0, height);
            if (matrix[randomX, randomY].GetComponent<ButtonElement>().bomb)
                i--;
            else
            {
                matrix[randomX, randomY].GetComponent<ButtonElement>().bomb = true;
                //matrix[randomX, randomY].GetComponent<Image>().color = Color.red;
            }

        }
        Cursor.lockState = CursorLockMode.None;
    }


    public int CheckBombs(int x, int y)
    {
        int cont = 0;
        
        if (x > 0)
        {
            if (y > 0 && matrix[x - 1, y - 1].GetComponent<ButtonElement>().bomb) cont++;
            if (matrix[x - 1, y].GetComponent<ButtonElement>().bomb) cont++;
            if (y < height - 1 && matrix[x - 1, y + 1].GetComponent<ButtonElement>().bomb) cont++;
        }
        if (x < width -1)
        {
            if (y > 0 && matrix[x + 1, y - 1].GetComponent<ButtonElement>().bomb) cont++;
            if (matrix[x + 1, y].GetComponent<ButtonElement>().bomb) cont++;
            if (y < height - 1 && matrix[x + 1, y + 1].GetComponent<ButtonElement>().bomb) cont++;
        }
        if (y > 0 && matrix[x, y - 1].GetComponent<ButtonElement>().bomb) cont++;
        if (y < height - 1 && matrix[x, y + 1].GetComponent<ButtonElement>().bomb) cont++;


        return cont;
    }
    public void GameOver()
    {
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (matrix[j, i].GetComponent<Button>().interactable) {
                    matrix[j, i].GetComponent<ButtonElement>().CheckBomb();
                }
            }
        }
        gameOverPanel.SetActive(true);
    }

    public void Replay()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
