using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitByArrow : MonoBehaviour
{
    void OnColliderEnter (Collider other)
    {
        Debug.Log (other.gameObject);
        if (other.name == "Arrow")
        {
            gameObject.SetActive(false);
        }
    }
}
