using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashMob_BladeCollision : MonoBehaviour
{
    HealthComponent playerVie;
    public Animator anim;
    GameObject player;
    public bool asBeenDeflected = false;
    public float knockback;

    void Start()
    {
        playerVie = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void Update()
    {
        if (asBeenDeflected)
        {
            anim.Play("Deflect");
            asBeenDeflected = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PlayerAttack.canDeflect)
        {
            playerVie.PlayerDeflected();
            asBeenDeflected = true;
        }
        else if (other.CompareTag("Player") && !PlayerAttack.canDeflect)
        {
            Vector3 direction = transform.position - player.transform.position;
            playerVie.PlayerGetHit(Random.Range(1,3));
            playerVie.GetComponent<Rigidbody>().AddForce(-direction * knockback, ForceMode.Impulse);
        }
    }
}
