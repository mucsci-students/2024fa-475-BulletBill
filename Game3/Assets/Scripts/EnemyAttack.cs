using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyControl script;
    public PauseGame pasueScript;
    public int health = 3; //(3 hits, 3rd kills)
    private int damage = 1;
    void Start()
    {
        pasueScript = GameObject.Find("GameManager").GetComponent<PauseGame>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            script.anim.SetBool("canWalk", false);
            script.isWalk = false;
            StartCoroutine(attack());
            script.anim.SetTrigger("attackTrigger");
            //Debug.Log("Test Pass");
            if (health == 0)
            {
                pasueScript.GameOver();
            }
            else if (health > 0)
            {
                health -= damage;
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
