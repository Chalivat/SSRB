using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    HealthComponent playerVie;
    public static int damage;

    void Start()
    {
        playerVie = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerAttack.canDeflect)
        {

        }
        else if(other.CompareTag("Player") && !PlayerAttack.canDeflect)
        {
            playerVie.vie -= damage;
        }
    }
}
