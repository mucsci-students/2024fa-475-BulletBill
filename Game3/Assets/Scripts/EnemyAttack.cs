using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyControl script;
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            script.anim.SetBool("canWalk", false);
            script.isWalk = false;
            StartCoroutine(attack());
            script.anim.SetTrigger("attackTrigger");
        }
    }

    IEnumerator attack()
    {
        yield return new WaitForSeconds (1f);
        script.anim.SetBool("canWalk", true);
        script.isWalk = true;
        
    }
}
