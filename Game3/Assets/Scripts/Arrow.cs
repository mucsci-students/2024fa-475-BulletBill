using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public Transform arrowSpawn;
    void FixedUpdate()
    {
        if (transform.position.y < -10f)
        {
            transform.position = new Vector3(arrowSpawn.transform.position.x, arrowSpawn.transform.position.y, arrowSpawn.transform.position.z);
        }
    }
}
