using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public EnnemiEpee epee;
    HealthComponent playerVie;

    void Start()
    {
        epee.GetComponent<EnnemiEpee>();
        playerVie = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerAttack.canDeflect)
        {
            epee.asBeenDeflected = true;
        }
        else if(other.CompareTag("Player") && !PlayerAttack.canDeflect)
        {
            playerVie.vie -= 1;
        }
    }
}
