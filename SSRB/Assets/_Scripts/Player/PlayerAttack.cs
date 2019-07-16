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
            //animSabre.Play("SabreHit");
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
            animShield.Play("shieldUp");
            if (chargingShield >= 2)
            {
                Instantiate(Tornado, transform.position, transform.rotation);
            }
            chargingShield = 0;
        }
    }
}
