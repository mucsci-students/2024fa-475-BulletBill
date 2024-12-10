using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeesPlayer : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy;
    public GameObject scriptObject;
    public EnemySpawn script;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        scriptObject = GameObject.FindGameObjectWithTag("EnemySpawn"); // Find the GameObject
        script = scriptObject.GetComponent<EnemySpawn>(); // Get the "MyScript" component from the object
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            if (!(script.GetComponent<EnemyControl>().target = script.Player))
                Shoot();
        }
    }
    void OnTriggerStay (Collider other)
    {
        if (other.tag == "Player")
        {
            if (!(script.GetComponent<EnemyControl>().target = script.Player))
                Shoot();
        }
    }
    void Shoot ()
    {
        RaycastHit hit;
        Vector3 enemyPos = enemy.transform.position;
        enemyPos.z += 1;
        Vector3 direction = player.transform.position - enemyPos; // Calculate direction vector
        //Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range);
        // if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, Mathf.Infinity, layerMask))
        if (Physics.Raycast(enemy.transform.position, direction, out hit))
        {
            Debug.Log(hit.transform.name);
            //hit.collider.gameObject.SetActive(false);

            if (hit.collider.gameObject.tag == "Player")
            {
                script.GetComponent<EnemyControl>().target = script.Player;
            }
            
        }
    }
}
