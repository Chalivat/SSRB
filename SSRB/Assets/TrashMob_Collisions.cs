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
    int randomRotation;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("anim");
        anim = GetComponent<Animator>();
        slider.maxValue = health;
        blade.enabled = false;
        particles.SetActive(false);
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
            Debug.Log("TAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            direction = transform.position - player.transform.position;
            Quaternion newRot = Quaternion.LookRotation(-direction);
            Vector3 nextRot = newRot.eulerAngles;
            nextRot.x = 0;
            transform.rotation = Quaternion.Euler(nextRot);
            
            agent.SetDestination(player.transform.position);
        }
        else if(!isHit && !isFollowing)
        {
            agent.SetDestination(transform.position);
        }

        if (isHit)
        {
            direction = transform.position - player.transform.position;
            agent.SetDestination(player.transform.position + (direction.normalized * 3));
        }
        else if (!isHit && !isFollowing)
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
