using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootScript : MonoBehaviour
{
    public Animator anim;
    GameObject player;
    public float knockback;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") && !PlayerAttack.canDeflect)
        {
            Vector3 dir = player.transform.position - transform.position;
            anim.SetTrigger("Combo");
            player.GetComponent<Rigidbody>().AddForce(dir * knockback, ForceMode.Impulse);
        }
    }
}
