using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] public AudioClip hurtSound;
    
    [SerializeField] public AudioClip healSound;
    public int playerHealth;
    public GameObject enemy;
    public GameObject hitScreen1;
    public GameObject hitScreen2;
    public PauseGame pauseScript;
    //public GameObject dieScreen;
    void Start()
    {
        playerHealth = 3;
        hitScreen1.SetActive(false);
        hitScreen2.SetActive(false);
        pauseScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
        //dieScreen.SetActive(false);
    }

    // Update is called once per frame
    public void changeHealth(int damage)
    {
        playerHealth -= damage;
        SoundFXManager.instance.PlaySoundFXClip(hurtSound, transform, 0.2f);

        if (playerHealth == 2)
        {
            hitScreen1.SetActive(true);
            hitScreen2.SetActive(false);
            
        }
        else if (playerHealth == 1)
        {
            hitScreen2.SetActive(true);
            hitScreen1.SetActive(false);
        }
        else
        {
            hitScreen1.SetActive(false);
            hitScreen2.SetActive(false);
            //dieScreen.SetActive(true);
            enemy = GameObject.FindGameObjectWithTag("Enemy");
            enemy.SetActive(false);
            pauseScript.GameOver();
        }
    }

    public void heal()
    {
        playerHealth = 3;
        SoundFXManager.instance.PlaySoundFXClip(healSound, transform, 0.3f);
        hitScreen1.SetActive(false);
        hitScreen2.SetActive(false);
    }
}
