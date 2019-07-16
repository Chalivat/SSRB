using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class Enemi_Distance : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    Vector3 direction;
    public Animator anim;
    public float ejection;

    public float initialTime;
    float shootTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootTime = initialTime;
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        LookRotation();
        Shoot();
    }

    void LookRotation()
    {
        direction = player.transform.position - transform.position;
        Quaternion newRot = Quaternion.LookRotation(direction);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        transform.rotation = Quaternion.Euler(nextRot);
    }

    void Shoot()
    {
        shootTime -= Time.deltaTime;

        if(shootTime <= 0)
        {
            anim.Play("Shoot");
            shootTime = initialTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Tornade"))
        {
            rb.AddForce(Vector3.up * ejection, ForceMode.Impulse);
        }
    }
}
