using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnnemiEpee_CollisionDetector : MonoBehaviour
{
    public int health;
    public int poise;
    public int poiseMin;
    public int poiseMiddle;
    public float ejection;
    public float knockback;
    public Slider healthBar;
    public Slider poiseBar;
    public float initialpoiseRecoveryTime;
    float poiseRecoveryTime;


    public NavMeshAgent agent;
    public GameObject particles;
    public string[] animationsCac;
    HealthComponent playerhealth;
    public SwordCollision swordCollision;
    public bool isAgro = false;
    public bool isDefensive = false;
    public bool isVulnerable = false;

    public BoxCollider sword;
    EnnemisEppee_V2 idle;
    DeflectImpact deflectImpact;
    Rigidbody rb;
    Animator anim;
    Vector3 direction;
    GameObject player;
    bool asBeenEjected = false;
    bool isFollowing = true;
    int CounterRandom;

    void Start()
    {
        particles.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerhealth = player.GetComponent<HealthComponent>();
        anim = GetComponent<Animator>();
        idle = anim.GetBehaviour<EnnemisEppee_V2>();
        deflectImpact = anim.GetBehaviour<DeflectImpact>();
        rb = GetComponent<Rigidbody>();
        healthBar.maxValue = health;
        poiseBar.maxValue = poise;
        poiseRecoveryTime = initialpoiseRecoveryTime;
    }
    
    void Update()
    {
        healthBar.value = health;
        poiseBar.value = poise;
        transform.localPosition = Vector3.zero;
        Poise();

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
            Quaternion newRot = Quaternion.LookRotation(-direction);
            Vector3 nextRot = newRot.eulerAngles;
            nextRot.x = 0;
            transform.rotation = Quaternion.Euler(nextRot);
            direction = transform.position - player.transform.position;
            agent.SetDestination(player.transform.position +direction.normalized);
        }
        else if(!isFollowing && swordCollision.asBeenDeflected)
        {
            agent.SetDestination(transform.position);
        }
    }

    void Poise()
    {
        poiseRecoveryTime -= Time.deltaTime;

        if(poiseRecoveryTime <= 0 && isAgro)
        {
            poise += 1;
            poiseRecoveryTime = initialpoiseRecoveryTime;
        }
        else if(poiseRecoveryTime <= 1 && isDefensive)
        {
            poise += 1;
            poiseRecoveryTime = initialpoiseRecoveryTime;
        }
        else if(poiseRecoveryTime <= 0 && isVulnerable)
        {
            poiseRecoveryTime = initialpoiseRecoveryTime;
        }


        if(poise > poiseMiddle)
        {
            isDefensive = false;
            isAgro = true;
            isVulnerable = false;
            anim.SetBool("isDefensive", false);
            anim.SetBool("isAfro", true);
            anim.SetBool("isVulnerable", false);
        }
        else if (poise <= poiseMiddle && poise >= poiseMin)
        {
            isDefensive = true;
            isAgro = false;
            isVulnerable = false;
            anim.SetBool("isDefensive", true);
            anim.SetBool("isAfro", false);
            anim.SetBool("isVulnerable", false);
        }
        else if(poise < poiseMin)
        {
            isDefensive = false;
            isAgro = false;
            isVulnerable = true;
            anim.SetBool("isDefensive", false);
            anim.SetBool("isAfro", false);
            anim.SetBool("isVulnerable", true);
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
            playerhealth.PlayerHit();
            direction = transform.position - player.transform.position;
            sword.GetComponent<BoxCollider>().enabled = false;
            agent.velocity = rb.velocity;
            if (poise <= 0)
            {
                rb.AddForce(direction * knockback/2, ForceMode.Impulse);
                anim.Play("Impact");
            }
            else
            {
                rb.AddForce(direction * knockback, ForceMode.Impulse);
            }
            health -= 1;
            poise -= 2;
        }
        else if (other.CompareTag("Sabre") && idle.isBlocking)
        {
            playerhealth.PlayerGetDeflected();
            CounterRandom = Random.Range(1, 3);
            poise -= 1;
            if(CounterRandom == 2)
            {
                anim.Play(animationsCac[Random.Range(0, animationsCac.Length)]);
            }

            if (poise <= 0)
            {
                rb.AddForce(direction * knockback / 2, ForceMode.Impulse);
                anim.Play("Impact");
            }
        }
    }

    public void NoCollider()
    {
        sword.enabled = false;
        particles.SetActive(false);
    }

    public void YesCollider()
    {
        sword.enabled = true;
        idle.isBlocking = false;
        particles.SetActive(true);
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
