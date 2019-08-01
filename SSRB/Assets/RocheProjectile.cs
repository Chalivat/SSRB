using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocheProjectile : MonoBehaviour
{
    float time;
    public float speed;

    Rigidbody rb;
    GameObject player;

    bool asShoot = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("anim");
        time = Random.Range(1, 4);
    }
    
    void Update()
    {
        time -= Time.deltaTime;

        if(time <= 0)
        {
            if (!asShoot)
            {
                asShoot = true;
                rb.AddForce((player.transform.position - transform.position) * speed, ForceMode.Impulse);
            }
        }
    }
}
