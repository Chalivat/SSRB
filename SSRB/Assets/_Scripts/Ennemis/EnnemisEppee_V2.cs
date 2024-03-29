﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.AI;

public class EnnemisEppee_V2 : StateMachineBehaviour
{
    public float speed;
    public float distanceMin;
    public float distanceMax;
    public bool onFloor = false;
    public BoxCollider foot;
    public Animator anim;
    public float minTime;
    public float maxTime;
    public string[] animations;
    public bool isBlocking = false;
    public bool asAttacked = false;
    DeflectImpact deflectImpact;
    NavMeshAgent agent;
    
    GameObject player;
    Rigidbody rb;
    Vector3 direction;
    Vector3 velocity;
    bool sweetspot = false;
    int sens = 0;
    float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.speed = 1;
        agent = animator.GetComponent<EnnemiEpee_CollisionDetector>().agent;
        deflectImpact = animator.GetBehaviour<DeflectImpact>();
        deflectImpact.isImpacted = false;
        player = GameObject.FindGameObjectWithTag("Player");
        rb = animator.GetComponent<Rigidbody>();
        anim = animator;
        foot = GameObject.FindGameObjectWithTag("Foot").GetComponent<BoxCollider>();
        foot.enabled = false;
        time = Random.Range(minTime, maxTime);
        isBlocking = true;
        agent.updateRotation = true;
        agent.speed = 2.2f;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ShootRaycast(animator);
        Move(animator);
        Attack();
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
    override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
    override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    void Move(Animator animator)
    {
        direction = animator.transform.position - player.transform.position;
        Quaternion newRot = Quaternion.LookRotation(-direction);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        animator.transform.rotation = Quaternion.Euler(nextRot);

        if (onFloor)
        {
            agent.SetDestination(player.transform.position + direction.normalized * distanceMin);

            if (Vector3.Distance(animator.transform.position, player.transform.position) > distanceMax)
            {
                Debug.Log("J'avance");
                sweetspot = false;
            }
            else if (Vector3.Distance(animator.transform.position, player.transform.position) >= distanceMin &&
                Vector3.Distance(animator.transform.position, player.transform.position) <= distanceMax)
            {
                Debug.Log("Je suis immobile");
                sweetspot = true;
                asAttacked = false;
            }
            
        }
        else
        {
            rb.velocity = Vector3.down * speed * Time.deltaTime / 3;
        }
        
    }

    void ShootRaycast(Animator animator)
    {
        RaycastHit hit;
        Debug.DrawRay(animator.transform.position, Vector3.down * 0.3f, Color.blue);
        if (Physics.Raycast(animator.transform.position, Vector3.down, out hit, 0.3f))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                onFloor = true;
            }
        }
        else
        {
            onFloor = false;
        }
    }

    void Attack()
    {
        if (sweetspot && !asAttacked)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                Debug.Log("Coucou");
                anim.Play(animations[Random.Range(0, animations.Length)]);
                time = 0;
            }
        }
        else
        {
            time = Random.Range(minTime, maxTime);
        }
    }
}
