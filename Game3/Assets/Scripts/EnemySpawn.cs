using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public class EnemySpawn : MonoBehaviour
{
    public GameObject Demon;
    public Vector3[] SpawnLocations;
    public Transform Player;

    void Start()
    {
        GameObject clone = Instantiate(Demon, SpawnLocations[Random.Range(0, 4)], Quaternion.identity);
        clone.GetComponent<EnemyControl>().target = Player;
    }
    // void Update()
    // {

    // }
}
