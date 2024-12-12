using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Demon; //prefab
    public Vector3[] SpawnLocations;
    public Transform[] Traversal;
    public Transform Player;
    // public bool isBasementOpen = false;
    public ShootArrow ShootArrowScript;
    public EnemySpawn EnemySpawnScript;
    public ObjectGrabRelease GrabReleaseScript;
    public SeesPlayer seesScript;
    public GameObject clone;
    public Transform currentLocation;
    public Transform stairs1;
    public Transform stairs2;
    //public Transform stairs2;
    public bool hitTaser = false;
    public bool hitArrow = false;
    private float timer = 0f;
    void Start()
    {
        respawn();
        clone.GetComponent<EnemyControl>().target = currentLocation;
        seesScript = Demon.transform.GetChild(4).GetComponent<SeesPlayer>();
    }

    void Update()
    {
        // Makes demon traverse the map to a location
        //if (clone.GetComponent<EnemyControl>().target != Player)
        if (seesScript.isFollowingPlayer == false)
        {
            if (Vector3.Distance(clone.transform.position, currentLocation.transform.position) < .25)
            {
                if (!GrabReleaseScript.isBasementDoorOpen)
                {
                    currentLocation = Traversal[Random.Range(0, 15)];
                }
                else
                {
                    currentLocation = Traversal[Random.Range(0, 21)];
                }
                    
                //Debug.Log(currentLocation);
                clone.GetComponent<EnemyControl>().target = currentLocation;
            } 
        }

        if (Vector3.Distance(clone.transform.position, stairs1.transform.position) < .8f)
        {
            print ("lowering enemy speed");
            clone.GetComponent<NavMeshAgent>().speed = .4f;
        }
        else if (Vector3.Distance(clone.transform.position, stairs2.transform.position) < .8f)
        {
            print ("lowering enemy speed");
            clone.GetComponent<NavMeshAgent>().speed = .4f;
        }
        else
        {
            print ("back to normal speed");
            clone.GetComponent<NavMeshAgent>().speed = .9f;
        }
        
        if (hitTaser)
        {
            timer += Time.deltaTime;
            if (timer >= 15f)
            {
                hitTaser = false;
                respawn();
                timer = 0f;
            }
        }
        if (hitArrow)
        {
            timer += Time.deltaTime;
            if (timer >= 20f)
            {
                hitArrow = false;
                respawn();
                timer = 0f;
            }
        }
    }
    void respawn()
    {
        if (!GrabReleaseScript.isBasementDoorOpen)
        {
            clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 4)], Quaternion.identity);
            currentLocation = Traversal[Random.Range(0, 15)];
            clone.GetComponent<EnemyControl>().target = currentLocation;
            clone.GetComponent<HitByArrow>().script = ShootArrowScript;
            clone.GetComponent<HitByArrow>().enemyScript = EnemySpawnScript;
        }
        else
        {
            clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 6)], Quaternion.identity);
            currentLocation = Traversal[Random.Range(0, 21)];
            clone.GetComponent<EnemyControl>().target = currentLocation;
            clone.GetComponent<HitByArrow>().script = ShootArrowScript;
            clone.GetComponent<HitByArrow>().enemyScript = EnemySpawnScript;
        }
    }
}
