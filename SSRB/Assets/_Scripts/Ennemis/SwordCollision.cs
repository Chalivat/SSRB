using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    HealthComponent playerVie;
    public static int damage;
    public bool asBeenDeflected = false;
    bool Block = false;
    float blockTime;

    void Start()
    {
        playerVie = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
    }
    
    void Update()
    {
        if (Block)
        {
            blockTime += Time.deltaTime;
            if(blockTime >= 0.5f)
            {
                blockTime = 0;
                Block = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerAttack.canDeflect && !Block)
        {
            asBeenDeflected = true;
            Block = true;
        }
        else if(other.CompareTag("Player") && !PlayerAttack.canDeflect)
        {
            playerVie.vie -= damage;
        }
    }
}
