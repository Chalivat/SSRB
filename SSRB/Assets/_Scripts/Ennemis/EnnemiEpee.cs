using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class EnnemiEpee : MonoBehaviour
{
    GameObject player;
    public float speed;
    public float time;
    public float distanceMin;
    Vector3 direction;
    public Animator anim;

    bool isReach = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!isReach)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            direction = player.transform.position - transform.position;
            Quaternion newRot = Quaternion.LookRotation(direction);
            Vector3 nextRot = newRot.eulerAngles;
            nextRot.x = 0;
            transform.rotation = Quaternion.Euler(nextRot);
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
}
