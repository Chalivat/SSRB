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
                if (!animSabre.GetBool("hit1"))
                {
                    animSabre.SetBool("hit1", true);
                    animSabre.SetBool("hit2", false);
                }
                else
                {
                    animSabre.SetBool("hit1", false);
                    animSabre.SetBool("hit2", true);
                }
            
            
        }

        if (Input.GetButton("Hit"))
        {
            chargingHit += Time.deltaTime;
            if (chargingHit >= .2f)
            {
                //animSabre.Play("SabreCharge");
                animSabre.SetBool("isCharging",true);
            }
        }

        if (Input.GetButtonUp("Hit"))
        {
            animSabre.SetBool("isCharging", false);
            if (chargingHit >= .5f)
            {
                animSabre.Play("SabreEstoc");
            }
            chargingHit = 0;

        }
    }

    public int index;
    private float timeSinceLastAttack;
    
    

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
