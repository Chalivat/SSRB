using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerAttack : MonoBehaviour
{
    public Animator animSabre;
    public Animator animShield;

    private float chargingShield;
    private float chargingHit;
    public ParticleSystem ShieldParticles;
    public GameObject Tornado;

    public static bool canDeflect;
    void Start()
    {
        ShieldParticles.Stop();
    }
    
    void Update()
    {
       hit();
       shieldUp();
    }

    void hit()
    {
        if (Input.GetButtonDown("Hit"))
        {
            animSabre.Play("SabreCharge");
        }

        if (Input.GetButton("Hit"))
        {
            chargingHit += Time.deltaTime;
        }

        if (Input.GetButtonUp("Hit"))
        {
            if (chargingHit >= 0.5f)
            {
                animSabre.Play("SabreEstoc");
            }
            else animSabre.Play("SabreHit");

            chargingHit = 0;
        }
    }

    private float deflectTime;
    public float maxDeflectTime;
    void shieldUp()
    {
        if (Input.GetButtonDown("Shield"))
        {
            animShield.Play("ShieldCharge");
            ShieldParticles.Play();
        }

        if (Input.GetButton("Shield"))
        {
            chargingShield += Time.deltaTime;
            Debug.Log(chargingShield);
        }
        else ShieldParticles.Stop();

        if (Input.GetButtonUp("Shield"))
        {
            canDeflect = true;
            animShield.Play("shieldUp");
            if (chargingShield >= 2)
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
