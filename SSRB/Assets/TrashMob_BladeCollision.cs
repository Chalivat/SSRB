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
        playerVie = GameObject.FindGameObjectWithTag("anim").GetComponent<HealthComponent>();
        player = GameObject.FindGameObjectWithTag("anim");
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
        if (other.CompareTag("anim") && PlayerAttack.canDeflect)
        {
            playerVie.PlayerDeflected();
            asBeenDeflected = true;
        }
        else if (other.CompareTag("anim") && !PlayerAttack.canDeflect)
        {
            Vector3 direction = transform.position - player.transform.position;
            playerVie.PlayerGetHit(Random.Range(1,3));
            playerVie.GetComponent<Rigidbody>().AddForce(-direction * knockback, ForceMode.Impulse);
        }
    }
}
