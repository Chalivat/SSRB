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
    public float fallSpeed;
    Vector3 direction;
    Vector3 velocity;
    public Animator anim;
    public bool asBeenDeflected = false;

    bool isReach = false;
    bool isFriendHere = false;
    public bool onFloor = false;
    float speed;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
        speed = Random.Range(speedMin, speedMax);
    }
    
    void Update()
    {
        rb.velocity = velocity.normalized * speed * Time.deltaTime;
        ShootRaycast();
        if (onFloor)
        {
            Move();
        }
        else
        {
            rb.velocity = Vector3.down * fallSpeed;
        }

        if (asBeenDeflected)
        {
            anim.Play("SwordBack");
            asBeenDeflected = false;
        }

        if (isFriendHere)
        {
            velocity += transform.right;
        }
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
            velocity = direction.normalized;
        }

        time += Time.deltaTime;
        if(time >= 5 && Vector3.Distance(transform.position, player.transform.position) >= distanceMin)
        {
            isReach = false;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= distanceMin)
        {
            isFriendHere = false;
            velocity = Vector3.zero;
            isReach = true;
            if(time <= Random.Range(10,20))
            {
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    
                }
                else
                {
                    if(time <= 1 && !asBeenDeflected)
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

    void ShootRaycast()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 0.1f,Color.blue, 0.1f);
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.1f))
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

        RaycastHit friendlyHit;
        Debug.DrawRay(transform.position, player.transform.position - transform.position, Color.blue);
        if (Physics.Raycast(transform.position,direction, out friendlyHit, direction.magnitude))
        {
            if (friendlyHit.transform.CompareTag("Swordman"))
            {
                isFriendHere = true;
            }
        }
        else
        {
            isFriendHere = false;
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
