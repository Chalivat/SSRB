using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions.Comparers;

public class PlayerAttack : MonoBehaviour
{
    public Animator anim;
    public Animator animSabre;
    public Animator animShield;
    public PlayerCanHit playerCanHit;
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
        attackNumber = 0;
        canHit = true;
    }

    void Update()
    {
        hit();
        shieldUp();
        Debug.Log("attackNumber : " + attackNumber);
        hit();
    }

    public bool canHit;
    
    void hit()
        {
            if (Input.GetButtonDown("Hit") && canHit)
            {
                if (attackNumber == 0)
                {
                    anim.SetTrigger("Combo");
                    anim.SetBool("wantToCombo", false);
            }

                else if (attackNumber == 1)
                {
                    anim.SetTrigger("HitLeft");
                    anim.SetBool("wantToCombo", false);
            }

                else if (attackNumber == 2)
                {
                    anim.SetBool("wantToCombo",true);
                    anim.SetTrigger("Combo");
                }
                
            }

            if (Input.GetAxisRaw("BigHit") >0)
            {
                anim.Play("SpinHit");
            }

        if (wantToHit)
        {
            time += Time.deltaTime;
            if (time >= MaxCoyoteHit)
            {
                time = 0;
                wantToHit = false;
                anim.SetInteger("AttackNumber", -1);
                attackNumber = 0;
                anim.ResetTrigger("Combo");
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
            animShield.Play("shieldUp");
            canDeflect = true;
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
