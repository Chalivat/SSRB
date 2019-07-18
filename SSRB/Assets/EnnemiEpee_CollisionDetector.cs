using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnnemiEpee_CollisionDetector : MonoBehaviour
{
    public int health;
    public float ejection;
    public float knockback;
    public Slider healthBar;

    BoxCollider sword;
    EnnemisEppee_V2 idle;
    DeflectImpact deflectImpact;
    Rigidbody rb;
    Animator anim;
    Vector3 direction;
    GameObject player;
    bool asBeenEjected = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        idle = anim.GetBehaviour<EnnemisEppee_V2>();
        deflectImpact = anim.GetBehaviour<DeflectImpact>();
        sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        healthBar.maxValue = health;
    }
    
    void Update()
    {
        healthBar.value = health;

        if(health <= 0)
        {
            Destroy(gameObject);
        }
        if (asBeenEjected)
        {
            rb.AddForce(Vector3.up * ejection, ForceMode.Impulse);
            asBeenEjected = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Tornade"))
        {
            asBeenEjected = true;
        }

        if (other.CompareTag("Sabre") && !idle.isBlocking)
        {
            direction = transform.position - player.transform.position;
            sword.GetComponent<BoxCollider>().enabled = false;
            if (!deflectImpact)
            {
                rb.AddForce(direction * knockback/2, ForceMode.Impulse);
                anim.Play("Impact");
            }
            else
            {
                rb.AddForce(direction * knockback, ForceMode.Impulse);
            }
            health -= 1;
        }
        else if (other.CompareTag("Sabre") && idle.isBlocking)
        {
            direction = transform.position - player.transform.position;
            rb.AddForce(direction * knockback/2, ForceMode.Impulse);
        }
    }
}
