using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfBounds : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void FixedUpdate()
    {
        if (transform.position.y < -3f)
        {
            transform.position = new Vector3(player.transform.position.x, player.transform.position.y + .35f, player.transform.position.z);
        }
    }
}
