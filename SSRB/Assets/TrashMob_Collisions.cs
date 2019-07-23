using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class TrashMob_Collisions : MonoBehaviour
{
    public NavMeshAgent agent;
    public BoxCollider blade;
    public GameObject particles;
    bool isFollowing = false;
    bool isHit = false;
    Vector3 direction;
    GameObject player;
    Animator anim;
    public Slider slider;

    public int health;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        slider.maxValue = health;
    }
    
    void Update()
    {
        slider.value = health;
        if(health <= 0)
        {
            Destroy(gameObject);
        }

        if (isFollowing)
        {
            if (isFollowing)
            {
                direction = transform.position - player.transform.position;
                agent.SetDestination(player.transform.position + direction.normalized * 0.5f);
            }
        }
        else
        {
            agent.SetDestination(transform.position);
        }

        if (isHit)
        {
            direction = transform.position - player.transform.position;
            agent.SetDestination(player.transform.position + (direction.normalized * 3));
        }
        else
        {
            agent.SetDestination(transform.position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sabre"))
        {
            health -= 1;
            anim.Play("Hit");
        }
    }

    public void NoCollider()
    {
        blade.enabled = false;
        particles.SetActive(false);
    }

    public void YesCollider()
    {
        blade.enabled = true;
        particles.SetActive(true);
    }

    public void isFollowingPlayer()
    {
        isFollowing = true;
    }

    public void isNotFollowing()
    {
        isFollowing = false;
    }

    public void IsHit()
    {
        isHit = true;
    }

    public void IsNotHit()
    {
        isHit = false;
        anim.Play("Idle");
    }
}
