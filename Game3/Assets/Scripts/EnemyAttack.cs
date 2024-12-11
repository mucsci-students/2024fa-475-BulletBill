using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyControl script;
    public PauseGame pauseScript;
    public PlayerHealth healthScript;
    public int damage = 1;
    private float timer = 0f;
    // public GameObject Damage1;
    // public GameObject Damage2;
    void Start()
    {
        // Damage1 = GameObject.FindGameObjectWithTag("UI1");
        // Damage2 = GameObject.FindGameObjectWithTag("UI2");
        healthScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        pauseScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            script.anim.SetBool("canWalk", false);
            script.isWalk = false;
            StartCoroutine(attack());
            script.anim.SetTrigger("attackTrigger");
            if (healthScript.playerHealth > 0)
            {
                healthScript.changeHealth(damage);
            }
            else
            {
                pauseScript.GameOver();
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            timer += Time.deltaTime;
            if (timer >= 1.5f)
            {
                script.anim.SetBool("canWalk", false);
                script.isWalk = false;
                StartCoroutine(attack());
                script.anim.SetTrigger("attackTrigger");
                if (healthScript.playerHealth > 0)
                {
                    healthScript.changeHealth(damage);
                }
                else
                {
                    pauseScript.GameOver();
                }
                timer = 0f;
            }
            
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        { 
            timer = 0f;
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(1f);
        script.anim.SetBool("canWalk", true);
        script.isWalk = true;
    }
}
