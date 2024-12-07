using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControl : MonoBehaviour
{
    public int radius;
    public int unfollowTime;
    public int waitTime;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target;

    void Update()
    {
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}
