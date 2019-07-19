using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    HealthComponent playerVie;
    GameObject player;
    public static int damage;
    public static int knockback;
    public bool asBeenDeflected = false;
    bool Block = false;
    float blockTime;

    void Start()
    {
        playerVie = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("Player");
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
            Vector3 direction = transform.position - player.transform.position;
            playerVie.PlayerGetHit(damage);
            playerVie.GetComponent<Rigidbody>().AddForce(-direction * knockback, ForceMode.Impulse);
        }
    }
}
