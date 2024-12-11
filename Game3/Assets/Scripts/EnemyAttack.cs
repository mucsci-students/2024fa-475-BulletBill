using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyControl script;
    public PauseGame pauseScript;
    public int health = 2; //(3 hits, 3rd kills)
    private int damage = 1;
    public GameObject Damage1;
    public GameObject Damage2;
    void Start()
    {
        Damage1 = GameObject.FindGameObjectWithTag("UI1");
        Damage2 = GameObject.FindGameObjectWithTag("UI2");
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
            if (health > 0)
            {
                health -= damage;
                if (health == 1)
                {
                    Damage1.SetActive(true);
                }
                else if (health == 0)
                {
                    Damage1.SetActive(true);
                    Damage2.SetActive(true);
                }
            }
            else
            {
                pauseScript.GameOver();
            }
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds(1f);
        script.anim.SetBool("canWalk", true);
        script.isWalk = true;

    }
}
