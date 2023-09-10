using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CarHealth : MonoBehaviour
{

    public Material mat;
    public Material[] originalMaterials;
    public Material[] finalMaterials;
    public Renderer rend;
    public Image lifeImage;
    public float factorLifeUI;

    bool isCarOverRoad;
    // Start is called before the first frame update
    void Start()
    {
        originalMaterials = new Material[rend.materials.Length];
        finalMaterials = new Material[rend.materials.Length];

        for (int i = 0; i < originalMaterials.Length; i++)
        {
            originalMaterials[i] = rend.materials[i];
        }

        for (int i=0; i<finalMaterials.Length; i++)
        {
            finalMaterials[i] = mat;
        }
    }

    public void ChangeMaterials(bool original)
    {
        if (original) rend.materials = originalMaterials;
        else rend.materials = finalMaterials;
        isCarOverRoad = original;
    }
    // Update is called once per frame
    void Update()
    {
        if (lifeImage.fillAmount == 0) return;
        if (isCarOverRoad && lifeImage.fillAmount < 1) lifeImage.fillAmount += Time.deltaTime * factorLifeUI;
        else if (!isCarOverRoad && lifeImage.fillAmount > 0) lifeImage.fillAmount -= Time.deltaTime * factorLifeUI;
    }

    void Death()
    {
        Debug.Log("Death");
    }
}
