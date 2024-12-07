using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByArrow : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.name == "Arrow")
        {
            gameObject.SetActive(false);
        }
    }
    // void OnCollisionEnter (Collision other)
    // {
    //     if (other.gameObject.name == "Arrow")
    //     {
    //         gameObject.SetActive(false);
    //     }
    // }
}
