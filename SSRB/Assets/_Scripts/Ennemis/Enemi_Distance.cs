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
    public bool onFloor = false;
    public float fallSpeed;

    public float initialTimeMin;
    public float initialTimeMax;
    float shootTime;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shootTime = Random.Range(initialTimeMin,initialTimeMax);
        rb = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        ShootRaycast();
        LookRotation();

        if (onFloor)
        {
            Shoot();
        }
        else
        {
            rb.velocity = Vector3.down * fallSpeed;
        }
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
            shootTime = Random.Range(initialTimeMin,initialTimeMax);
        }
    }

    void ShootRaycast()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position + new Vector3(0, 0.1f, 0), Vector3.down * 0.1f, Color.blue);
        if (Physics.Raycast(transform.position + new Vector3(0,0.1f,0), Vector3.down, out hit, 0.2f))
        {
            if (hit.transform.CompareTag("Ground"))
            {
                onFloor = true;
            }
        }
        else
        {
            onFloor = false;
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
