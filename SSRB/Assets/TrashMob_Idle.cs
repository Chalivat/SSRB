using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TrashMob_Idle : StateMachineBehaviour
{
    NavMeshAgent agent;
    Vector3 direction;
    GameObject player;
    Rigidbody rb;
    bool onFloor = false;
    bool sweetspot = false;
    bool asAttacked = false;
    bool asFriendDetected = false;
    float rotateTime;

    public float distanceMin;
    public float distanceMax;
    public float speed;
    public float time;
    public float minTime;
    public float maxTime;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<TrashMob_Collisions>().agent;
        player = GameObject.FindGameObjectWithTag("anim");
        agent.speed = 4;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Move(animator);
        Attack(animator);
        ShootRayCast(animator);

        if (asFriendDetected)
        {
            rotateTime += Time.deltaTime;
            int rotateDirection = Random.Range(1, 3);
            if (rotateTime <= 1)
            {
                    agent.Move(animator.transform.right * 0.1f);
            }
            else
            {
                rotateTime = 0;
                asFriendDetected = false;
            }
        }
    }

    void Move(Animator animator)
    {
        direction = animator.transform.position - player.transform.position;
        Quaternion newRot = Quaternion.LookRotation(-direction);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        animator.transform.rotation = Quaternion.Euler(nextRot);

        Debug.Log("TY EST MOCHE");
        agent.SetDestination(player.transform.position + direction.normalized * distanceMin);

        if (Vector3.Distance(animator.transform.position, player.transform.position) > distanceMax)
        {
            Debug.Log("J'avance");
            sweetspot = false;
        }
        else if (Vector3.Distance(animator.transform.position, player.transform.position) >= distanceMin &&
            Vector3.Distance(animator.transform.position, player.transform.position) <= distanceMax)
        {
            Debug.Log("Je suis immobile");
            sweetspot = true;
            asAttacked = false;
        }
    }

    void Attack(Animator animator)
    {
        if (sweetspot && !asAttacked)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                animator.Play("Swing");
                time = 0;
            }
        }
        else
        {
            time = Random.Range(minTime, maxTime);
        }
    }

    void ShootRayCast(Animator animator)
    {
        RaycastHit hit;
        Debug.DrawRay(animator.transform.position, - direction * direction.magnitude, Color.blue);
        if (Physics.Raycast(animator.transform.position, -direction.normalized, out hit, direction.magnitude))
        {
            if (hit.transform.CompareTag("Swordman"))
            {
                asFriendDetected = true;
            }
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
