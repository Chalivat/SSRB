﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnnemisEppee_V2 : StateMachineBehaviour
{
    public float speed;
    public float distanceMin;
    public float distanceMax;
    public bool onFloor = false;
    public BoxCollider sword;
    public Animator anim;
    public float minTime;
    public float maxTime;
    public string[] animations;


    GameObject player;
    Rigidbody rb;
    Vector3 direction;
    Vector3 velocity;
    bool sweetspot = false;
    int sens = 0;
    float time;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = animator.GetComponent<Rigidbody>();
        anim = animator;
        sword = animator.gameObject.GetComponentInChildren<BoxCollider>();
        sword.enabled = false;
        time = Random.Range(minTime, maxTime);
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
        direction = player.transform.position - animator.transform.position;
        Quaternion newRot = Quaternion.LookRotation(direction);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        animator.transform.rotation = Quaternion.Euler(nextRot);

        if (onFloor)
        {
            /*Debug.Log(Vector3.Distance(animator.transform.position, player.transform.position));
            Debug.Log(isBackward);*/
            //Debug.Log(rb.velocity);

            if (Vector3.Distance(animator.transform.position, player.transform.position) >= distanceMax)
            {
                Debug.Log("J'avance");
                sens = 1;
                velocity = direction;
                sweetspot = false;
            }
            else if (Vector3.Distance(animator.transform.position, player.transform.position) <= distanceMax &&
                Vector3.Distance(animator.transform.position, player.transform.position) >= distanceMin)
            {
                Debug.Log("Je suis immobile");
                sweetspot = true;
                sens = 1;
                velocity = Vector3.zero;
            }
            else if (Vector3.Distance(animator.transform.position, player.transform.position) <= distanceMin)
            {
                Debug.Log("Je recule putain de ta mere");
                velocity = direction;
                sens = -5;
            }

            rb.velocity = velocity.normalized * speed * Time.deltaTime * sens;
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
        if (sweetspot)
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