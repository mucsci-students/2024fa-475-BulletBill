using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class StairSlowSpeed : MonoBehaviour
{
    void OnTriggerEnter (Collider other)
    {
        print (other.transform.tag);
        if (other.tag == "Radius")
        {
            //Debug.Log ("Speed was lowered");
            //other.GetComponent<NavMeshAgent>().speed = .1f;
            other.transform.parent.gameObject.GetComponent<NavMeshAgent>().speed = .4f;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Radius")
        {
            //Debug.Log ("Speed was raised");
            //other.GetComponent<NavMeshAgent>().speed = .8f;
            other.transform.parent.gameObject.GetComponent<NavMeshAgent>().speed = .9f;
        }
    }
}
