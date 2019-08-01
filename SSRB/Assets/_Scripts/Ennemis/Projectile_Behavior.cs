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
        playerVie = GameObject.FindGameObjectWithTag("anim").GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("anim");
        transform.LookAt(player.transform.position);
        rb.AddForce(transform.forward * speed, ForceMode.Force);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("anim") && !PlayerAttack.canDeflect)
        {
            playerVie.vie -= 1;
            Destroy(gameObject);
        }
        else if(other.CompareTag("anim") && PlayerAttack.canDeflect)
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
