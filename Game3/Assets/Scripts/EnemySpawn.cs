using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

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
    public GameObject clone;
    private Transform currentLocation;
    public bool hitTaser = false;
    public bool hitArrow = false;
    private float timer = 0f;
    void Start()
    {
        respawn();
    }

    void Update()
    {
        // Makes demon traverse the map to a location
        if (clone.GetComponent<EnemyControl>().target != Player)
        {
            if (Vector3.Distance(clone.transform.position, currentLocation.transform.position) < .25)
            {
                //Debug.Log(currentLocation);
                if (!GrabReleaseScript.isBasementDoorOpen)
                {
                    currentLocation = Traversal[Random.Range(0, 15)];
                }
                else
                {
                    currentLocation = Traversal[Random.Range(0, 20)];
                }
                    
                //Debug.Log(currentLocation);
                clone.GetComponent<EnemyControl>().target = currentLocation;
            } 
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
                hitTaser = false;
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
            //clone.GetComponent<EnemyControl>().target = Player;
            clone.GetComponent<HitByArrow>().script = ShootArrowScript;
            clone.GetComponent<HitByArrow>().enemyScript = EnemySpawnScript;
        }
        else
        {
            clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 6)], Quaternion.identity);
            currentLocation = Traversal[Random.Range(0, 20)];
            clone.GetComponent<EnemyControl>().target = currentLocation;
            //clone.GetComponent<EnemyControl>().target = Player;
            clone.GetComponent<HitByArrow>().script = ShootArrowScript;
            clone.GetComponent<HitByArrow>().enemyScript = EnemySpawnScript;
        }
    }
}
