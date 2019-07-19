using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnnemiEpee_CollisionDetector : MonoBehaviour
{
    public int health;
    public float ejection;
    public float knockback;
    public Slider healthBar;
    public NavMeshAgent agent;

    public BoxCollider sword;
    EnnemisEppee_V2 idle;
    DeflectImpact deflectImpact;
    Rigidbody rb;
    Animator anim;
    Vector3 direction;
    GameObject player;
    bool asBeenEjected = false;
    bool isFollowing = true;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        idle = anim.GetBehaviour<EnnemisEppee_V2>();
        deflectImpact = anim.GetBehaviour<DeflectImpact>();
        rb = GetComponent<Rigidbody>();
        healthBar.maxValue = health;
    }
    
    void Update()
    {
        healthBar.value = health;
        transform.localPosition = Vector3.zero;

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        if (asBeenEjected)
        {
            rb.AddForce(Vector3.up * ejection, ForceMode.Impulse);
            asBeenEjected = false;
        }

        if (isFollowing)
        {
            direction = transform.position - player.transform.position;
            agent.SetDestination(player.transform.position +direction.normalized);
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
            agent.velocity = rb.velocity;
            if (!deflectImpact.isImpacted)
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

    public void NoCollider()
    {
        sword.enabled = false;
    }

    public void YesCollider()
    {
        sword.enabled = true;
        idle.isBlocking = false;
    }

    public void isBlockingTrue()
    {
        idle.isBlocking = true;
    }
    public void isFollowingPlayer()
    {
        isFollowing = true;
    }

    public void isNotFollowing()
    {
        isFollowing = false;
    }
}
