using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeesPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject scriptObject;
    public EnemySpawn script;
    public bool isFollowingPlayer = false;
    private Vector3 enemyPos;
    private Vector3 playerPos;
    void Start()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
        scriptObject = GameObject.FindGameObjectWithTag("EnemySpawn"); // Find the GameObject
        script = scriptObject.GetComponent<EnemySpawn>(); // Get the "EnemySpawn" component from the object
    }


    void OnTriggerEnter (Collider other)
    {
        if (other.transform.name == "Player1")
        {
            if (!isFollowingPlayer)
                Shoot();
        }
    }
    void OnTriggerStay (Collider other)
    {

        if (other.transform.name == "Player1")
        {
           if (!isFollowingPlayer)
                Shoot();
                
        }
    }
    void Shoot ()
    {
        RaycastHit hit;
        enemyPos = enemy.transform.position;
        playerPos = player.transform.position;
        playerPos.y += .3f;
        enemyPos.y += .3f;
        Vector3 direction = (playerPos - enemyPos).normalized; // Calculate direction vector
        //Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        // if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        if (Physics.Raycast(enemy.transform.position, direction, out hit, Mathf.Infinity))
        {
            //Debug.DrawLine(enemy.transform.position, hit.point, Color.red, 0.5f);
            //hit.collider.gameObject.SetActive(false);

            //if (hit.collider.gameObject.tag == "Player")
            if (hit.collider.gameObject.tag == "Player")
            {
                Debug.Log("Tracking Player");
                script.clone.GetComponent<EnemyControl>().target = script.Player;
                isFollowingPlayer = true;
            }
            
        }
    }

    // private void OnDrawGizmos()

    // {

    //     Gizmos.color = Color.red; // Set gizmo color

    //     Gizmos.DrawWireSphere(enemyPos, .5f); // Draw a wire sphere at the specified position

    // }
}
