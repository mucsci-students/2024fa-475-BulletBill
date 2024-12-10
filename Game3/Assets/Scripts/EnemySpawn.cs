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
    public bool isBasementOpen = false;
    public ShootArrow ShootArrowScript;
    private GameObject clone;
    private Transform currentLocation;
    public bool hitTaser = false;
    public bool hitArrow = false;
    void Start()
    {
        clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 4)], Quaternion.identity);
        currentLocation = Traversal[Random.Range(0, 15)];
        clone.GetComponent<EnemyControl>().target = currentLocation;
        //clone.GetComponent<EnemyControl>().target = Player;
        clone.GetComponent<HitByArrow>().script = ShootArrowScript;
    }

    void Update()
    {
        if (Vector3.Distance(clone.transform.position, currentLocation.transform.position) < .25)
        {

            Debug.Log(currentLocation);
            currentLocation = Traversal[Random.Range(0, 15)];
            Debug.Log(currentLocation);
            clone.GetComponent<EnemyControl>().target = currentLocation;
        }
    }
    void respawn()
    {
        if (!isBasementOpen)
        {
            clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 4)], Quaternion.identity);
        }
        else
        {
            clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 6)], Quaternion.identity);
        }
    }
}
