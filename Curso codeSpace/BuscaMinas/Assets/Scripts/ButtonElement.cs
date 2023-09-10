using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonElement : MonoBehaviour
{
    public int x, y;
    public bool bomb;

    public void SetValues(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public void CheckBomb()
    {
        if (!GetComponent<Button>().interactable)
            return;
        GetComponent<Button>().interactable = false;
        if (bomb)
        {
            GetComponent<Image>().color = Color.red;
            GameManager.gm.GameOver();

        }
        else
        {
            int numBombs = transform.parent.GetComponent<GameManager>().CheckBombs(x, y);
            if (numBombs > 0)
            {
                GetComponentInChildren<Text>().text = numBombs.ToString();
                switch (numBombs)
                {
                    case 1:
                        GetComponentInChildren<Text>().color = Color.blue;
                        break;

                    case 2:
                        GetComponentInChildren<Text>().color = Color.green;
                        break;
                    case 3:
                        GetComponentInChildren<Text>().color = Color.yellow;
                        break;
                    case 4:
                        GetComponentInChildren<Text>().color = Color.magenta;
                        break;
                    case 5:
                        GetComponentInChildren<Text>().color = Color.red;
                        break;
                    default:
                        break;
                }
            }
            else {
                GetComponent<Image>().color = Color.grey;
                if (x > 0)
                {
                    GameManager.gm.matrix[x - 1, y].GetComponent<ButtonElement>().CheckBomb();
                    if (y > 0)
                        GameManager.gm.matrix[x - 1, y - 1].GetComponent<ButtonElement>().CheckBomb();
                    if (y < GameManager.gm.height - 1)
                        GameManager.gm.matrix[x -1, y + 1].GetComponent<ButtonElement>().CheckBomb();

                }
                if (x < GameManager.gm.width - 1)
                {
                    GameManager.gm.matrix[x + 1 , y].GetComponent<ButtonElement>().CheckBomb();
                    if (y > 0)
                        GameManager.gm.matrix[x + 1, y - 1].GetComponent<ButtonElement>().CheckBomb();
                    if (y < GameManager.gm.height - 1)
                        GameManager.gm.matrix[x + 1, y + 1].GetComponent<ButtonElement>().CheckBomb();
                }
                if (y > 0)
                    GameManager.gm.matrix[x, y - 1].GetComponent<ButtonElement>().CheckBomb();
                if (y < GameManager.gm.height - 1)
                    GameManager.gm.matrix[x, y + 1].GetComponent<ButtonElement>().CheckBomb();
            }
        }
    }
}
