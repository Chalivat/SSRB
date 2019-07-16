using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemi_Distance : MonoBehaviour
{
    GameObject player;
    Vector3 direction;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        direction = player.transform.position - transform.position;
        Quaternion newRot = Quaternion.LookRotation(direction);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x *= -1;
        transform.rotation = Quaternion.Euler(nextRot);
    }
}
