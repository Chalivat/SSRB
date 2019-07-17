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
    public GameObject sword;
    Vector3 direction;
    Vector3 velocity;
    public Animator anim;
    public bool asBeenDeflected = false;
    bool asBeenHit = false;

    bool isReach = false;
    bool isFriendHere = false;
    public bool onFloor = false;
    float speed;
    public int health;

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
        Ifs();
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
            if(time <= Random.Range(5,20))
            {
                if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1)
                {
                    
                }
                else
                {
                    if(time <= 1 && !asBeenDeflected)
                    {
                        sword.GetComponent<BoxCollider>().enabled = true;
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

    private void Ifs()
    {
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
            sword.GetComponent<BoxCollider>().enabled = false;
            anim.Play("SwordBack");
            asBeenDeflected = false;
        }

        if (isFriendHere)
        {
            velocity += transform.right;
        }

        if (asBeenHit)
        {
            asBeenHit = false;
            sword.GetComponent<BoxCollider>().enabled = false;
            anim.Play("SwordBack");
            health -= 1;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ShootRaycast()
    {
        RaycastHit hit;
        Debug.DrawRay(transform.position, Vector3.down * 0.1f,Color.blue);
        if(Physics.Raycast(transform.position, Vector3.down, out hit, 0.3f))
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
        if (other.CompareTag("Sabre"))
        {
            asBeenHit = true;
        }
    }


}
