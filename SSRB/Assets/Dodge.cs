using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dodge : MonoBehaviour
{
    public float dodgeTime;
    private float time;
    public float dodgeForce;
    private bool isDodging;
    private Rigidbody rb;
    private CharacterController controller;

    public RotateCamera rotateCamera;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();
        rotateCamera = FindObjectOfType<RotateCamera>();
    }
    
    void FixedUpdate()
    {
        Dodging();
    }

    void Dodging()
    {
        if (Input.GetButtonDown("Dodge"))
        {
            isDodging = true;
            controller.enabled = false;
            rotateCamera.autoAimSpeed = 2;
            CameraShake.FreezeTime(-3);
            int multiplier;
            if (Input.GetAxis("Strafe") > .2f)
                multiplier = 1;
            else if (Input.GetAxis("Strafe") <-.2f)
                multiplier = -1;
            else
                multiplier = 0;

            rb.AddForce(transform.right * multiplier * dodgeForce,ForceMode.VelocityChange);
        }

        if (isDodging)
        {
            time++;
            if (time >= dodgeTime)
            {
                time = 0;
                isDodging = false;
                controller.enabled = true;
                rotateCamera.autoAimSpeed = 7;
            }
        }
    }
}
