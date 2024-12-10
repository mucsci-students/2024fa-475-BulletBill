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
    public Transform Player;
    public bool isBasementOpen = false;
    public ShootArrow ShootArrowScript;

    void Start()
    {
        GameObject clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 4)], Quaternion.identity);
        clone.GetComponent<EnemyControl>().target = Player;
        clone.GetComponent<HitByArrow>().script = ShootArrowScript;
    }

    void respawn()
    {
        if (!isBasementOpen)
        {
            GameObject clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 4)], Quaternion.identity);
        }
        else
        {
            GameObject clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 6)], Quaternion.identity);
        }
    }
}
