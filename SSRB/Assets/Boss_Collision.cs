using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Collision : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject[] slots;
    public GameObject projectile;
    bool isFollowing = false;

    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    
    void Update()
    {
        if (isFollowing)
        {
            Vector3 direction = transform.position - player.transform.position;
            Quaternion newRot = Quaternion.LookRotation(-direction);
            Vector3 nextRot = newRot.eulerAngles;
            nextRot.x = 0;
            transform.rotation = Quaternion.Euler(nextRot);
        }
    }

    public void RangedAttack()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            Instantiate(projectile, slots[i].transform.position, Quaternion.identity);
        }
    }

    public void IsFollowing()
    {
        isFollowing = true;
    }
    public void IsNotFollowing()
    {
        isFollowing = false;
    }
}
