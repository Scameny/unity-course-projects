using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public Slider slider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed;
    public Color flashColor;

    Animator anim;
    AudioSource audioSource;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;

    bool isDead;
    bool damaged;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerMovement = GetComponent<PlayerMovement>();
        playerShooting = GetComponentInChildren<PlayerShooting>();

    }

    private void Start()
    {
        currentHealth = maxHealth;
        slider.maxValue = maxHealth;
        slider.value = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        if (damaged) damageImage.color = flashColor;
        else damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        damaged = false;
    }

    public void TakeDamage(int amount)
    {
        if (isDead) return;
        damaged = true;
        currentHealth -= amount;
        slider.value = currentHealth;
        audioSource.Play();
        if (currentHealth <= 0 && !isDead) Death();
    }

    void Death()
    {
        isDead = true;
        anim.SetTrigger("Death");
        audioSource.clip = deathClip;
        audioSource.Play();
        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel()
    {
        GameObject.FindGameObjectWithTag("GameController").GetComponent<LoadSaveData>().SaveData();
        UnityEngine.SceneManagement.SceneManager.LoadScene("SampleScene");
    }
}
