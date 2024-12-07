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
    public Animator anim;
    public bool isWalk;
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform target;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.SetBool("canWalk", true);
        isWalk = true;
    }

    void Update()
    {
        if (target != null && isWalk)
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
