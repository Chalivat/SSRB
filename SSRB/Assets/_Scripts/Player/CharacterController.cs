﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    public Camera cam;
    public Animator Player;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
    }
    
    void FixedUpdate()
    {
        Move();
        Rotate();
    }

    void Move()
    {
        
        float x = Input.GetAxis("Strafe");
        float z = -Input.GetAxis("Forward");


        Vector3 direction = new Vector3(x, 0, z);
        Vector3 bis = direction;
        Quaternion newRot = Quaternion.LookRotation(cam.transform.forward);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        nextRot.z = 0;
        newRot = Quaternion.Euler(nextRot);
        direction = newRot * direction;
        if (direction.magnitude >= .5f)
        {
            rb.velocity = direction * speed;
            if (Lock.Target)
            {
                Player.SetBool("isStrafing", true);
                Player.SetBool("isRunning", false);
            }
            else
            {
                Player.SetBool("isRunning",true);
                Player.SetBool("isStrafing", false);
            }
        }
        else
        {
            Player.SetBool("isRunning",false);
            Player.SetBool("isStrafing", false);
        }
        adaptStrafing();
    }

    void Rotate()
    {
        Quaternion newRot = Quaternion.LookRotation(rb.velocity);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        nextRot.z = 0;
        newRot = Quaternion.Euler(nextRot);

        if (rb.velocity.magnitude >= .2f && !Lock.Target)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation,newRot,0.2f);
        }
        else if (Lock.Target)
        {
            Locked();
        }
    }

    void Locked()
    {
        Quaternion newRot = Quaternion.LookRotation(Lock.Target.transform.position - transform.position);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        nextRot.z = 0;
        newRot = Quaternion.Euler(nextRot);
        transform.rotation = Quaternion.Lerp(transform.rotation, newRot, 0.5f);
    }

    void adaptStrafing()
    {
        if (Input.GetAxis("Strafe") > 0)
        {
            Player.SetFloat("StrafingSpeed", -1);
        }
        else Player.SetFloat("StrafingSpeed", 1);
    }
}
