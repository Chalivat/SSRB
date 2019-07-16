using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnnemiEpee : MonoBehaviour
{
    Rigidbody rb;
    GameObject player;
    public float speedMin;
    public float speedMax;
    public float time;
    public float distanceMin;
    public float ejection;
    Vector3 direction;
    public Animator anim;

    bool isReach = false;
    float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(speedMin, speedMax);
    }
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!isReach)
        {
            direction = player.transform.position - transform.position;
            Quaternion newRot = Quaternion.LookRotation(direction);
            Vector3 nextRot = newRot.eulerAngles;
            nextRot.x = 0;
            transform.rotation = Quaternion.Euler(nextRot);
            rb.velocity = direction.normalized *speed * Time.deltaTime;
        }

        time += Time.deltaTime;
        if(time >= 5 && Vector3.Distance(transform.position, player.transform.position) >= distanceMin)
        {
            isReach = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= distanceMin)
        {
            isReach = true;
            if(time <= 5)
            {
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    
                }
                else
                {
                    if(time <=3)
                    {
                        anim.Play("Sword");
                    }
                }
            }
            else
            {
                time = 0;
                isReach = false;
            }
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
