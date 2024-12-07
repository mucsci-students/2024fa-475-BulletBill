using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    private int radius;
    private int unfollowTime;
    private int waitTime;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target;

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }

    void OnTriggerEnter()
    {
        Debug.Log("Player Hit!");
    }
    void OnTriggerStay()
    {

    }
}
