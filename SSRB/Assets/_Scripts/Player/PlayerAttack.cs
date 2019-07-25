﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerAttack : MonoBehaviour
{
    public Animator Player;
    public Animator animSabre;
    public Animator animShield;

    private float chargingShield;
    private float chargingHit;
    public ParticleSystem ShieldParticles;
    private ParticleSystem.MainModule psMain;
    public GameObject Tornado;

    public static bool canDeflect;

    public int attackNumber;

    public bool wantToHit;
    public float MaxCoyoteHit;
    float time;

    public bool isHit;

    void Start()
    {
        psMain = ShieldParticles.main;
        ShieldParticles.Stop();
        GameObject.FindGameObjectWithTag("Sabre").GetComponent<BoxCollider>().enabled = false;
    }

    void Update()
    {
        hit();
        shieldUp();
        Debug.Log("attackNumber : " + attackNumber);

        //Debug.Log("State : " + Sabre.GetComponent<BoxCollider>().enabled);
        hit();
    }

    void hit()
        {
            if (Input.GetButtonDown("Hit"))
            {
                Player.SetTrigger("Combo");
                if (attackNumber == 0)
                {
                   Player.SetInteger("AttackNumber",0);
                   Player.Play("Hit");
                }   
                else Player.SetInteger("AttackNumber",1);

                wantToHit = true;
            }

            if (Input.GetAxisRaw("BigHit") >0)
            {
                Player.Play("SpinHit");
            }

        if (wantToHit)
        {
            time += Time.deltaTime;
            if (time >= MaxCoyoteHit)
            {
                time = 0;
                wantToHit = false;
                Player.SetInteger("AttackNumber", 0);
                
            }
        }

        }
   

    public int index;
    private float timeSinceLastAttack;



    private float deflectTime;
    public float maxDeflectTime;
    public GameObject shieldCharged;

    void shieldUp()
    {

        if (Input.GetButtonDown("Shield"))
        {
            animShield.Play("ShieldCharge");
            ShieldParticles.Play();
            psMain.startColor = Color.yellow;
            canDeflect = true;
        }

        if (Input.GetButton("Shield"))
        {
            chargingShield += Time.deltaTime;
            if (chargingShield >= 1.2f && chargingShield < 1.3)
            {
                psMain.startColor = Color.cyan;
                Instantiate(shieldCharged, ShieldParticles.transform.position, transform.rotation);
            }
        }
        else ShieldParticles.Stop();

        if (Input.GetButtonUp("Shield"))
        {
            animShield.Play("shieldUp");
            if (chargingShield >= 1.2)
            {
                Instantiate(Tornado, transform.position, transform.rotation);
            }

            chargingShield = 0;
        }

        if (canDeflect)
        {
            deflectTime += Time.deltaTime;
            if (deflectTime >= maxDeflectTime)
            {
                deflectTime = 0;
                canDeflect = false;
            }
        }
    }
    

    
}
