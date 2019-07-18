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

    public float freezeTime;

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

        //Debug.Log("State : " + Sabre.GetComponent<BoxCollider>().enabled);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            TimeStop();
        }

        TimeGo();
    }

    void hit()
    {
        if (Input.GetButtonDown("Hit"))
        {
            Player.Play("Hit");
            
        }

        if (Input.GetButton("Hit"))
        {

        }

        if (Input.GetButtonUp("Hit"))
        {

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
            canDeflect = true;
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


    private int frameCount;

    void TimeStop()
    {
        Time.timeScale = 0.1f;
        frameCount = 0;
    }

    void TimeGo()
    {
        if (frameCount < 5)
        {
            frameCount++;
        }
        else Time.timeScale = 1;
    }

    
    
}
