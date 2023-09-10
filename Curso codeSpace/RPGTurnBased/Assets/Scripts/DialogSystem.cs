using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogSystem : MonoBehaviour
{
    [TextArea]
    public string[] dialog;
    public GameObject panelDialog;
    public Text textDialog, textName;
    public int idItem;
    public float speedText;
    int counter=0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ShowDialog(counter));
            counter++;
        }
    }


    public IEnumerator ShowDialog(int i)
    {
        if (idItem != 0 && i >= dialog.Length)
        {
            Player.player.AddItem(idItem);
            counter = 0;
            panelDialog.SetActive(false);
            Player.player.stopPlayer = false;
            enabled = false;
        }
        else if(i >= dialog.Length)
        {
            counter = 0;
            panelDialog.SetActive(false);
            Player.player.stopPlayer = false;
            enabled = false;
        }
        else
        {
            panelDialog.SetActive(true);
            textName.gameObject.SetActive(true);
            Player.player.stopPlayer = true;
            textName.text = transform.name;
            panelDialog.SetActive(true);
            textDialog.text = "";
            for (int j = 0; j < dialog[i].Length; j++)
            {
                textDialog.text += dialog[i][j];
                yield return new WaitForSeconds(speedText);
            }
        }
       
    }
}
