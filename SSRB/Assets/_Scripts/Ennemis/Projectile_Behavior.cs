using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Behavior : MonoBehaviour
{
    GameObject player;
    Rigidbody rb;
    public float speed;
    bool asBeenDeflected = false;
    HealthComponent playerVie;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerVie = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("Player");
        transform.LookAt(player.transform.position);
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !PlayerAttack.canDeflect)
        {
            playerVie.vie -= 1;
            Destroy(gameObject);
        }
        else if(other.CompareTag("Player") && PlayerAttack.canDeflect)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(-transform.forward * speed, ForceMode.Force);
            asBeenDeflected = true;
        }

        if(other.CompareTag("Distance") && asBeenDeflected)
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
