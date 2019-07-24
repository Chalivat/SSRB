using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Boss_Idle : StateMachineBehaviour
{
    NavMeshAgent agent;
    GameObject player;
    Vector3 direction;

    public float distanceMin;
    public float distanceMax;
    public string[] animations;

    bool sweetspot = false;
    bool tooFar = false;
    bool tooClose = false;
    float timeBetweenJumps;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<Boss_Collision>().agent;
        player = GameObject.FindGameObjectWithTag("Player");
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Move(animator);
        Attack(animator);

        if (tooClose)
        {
            timeBetweenJumps += Time.deltaTime;

            if(timeBetweenJumps >= 1.5f)
            {
                animator.Play("Tourbillon");
            }
        }
        else
        {
            timeBetweenJumps = 0;
        }
    }


    void Move(Animator animator)
    {
        direction = animator.transform.position - player.transform.position;
        Quaternion newRot = Quaternion.LookRotation(-direction);
        Vector3 nextRot = newRot.eulerAngles;
        nextRot.x = 0;
        animator.transform.rotation = Quaternion.Euler(nextRot);

        if (Vector3.Distance(animator.transform.position, player.transform.position) > distanceMax)
        {
            sweetspot = false;
            tooFar = true;
            tooClose = false;
        }
        else if (Vector3.Distance(animator.transform.position, player.transform.position) >= distanceMin &&
            Vector3.Distance(animator.transform.position, player.transform.position) <= distanceMax)
        {
            sweetspot = true;
            tooFar = false;
            tooClose = false;
        }
        else if(Vector3.Distance(animator.transform.position, player.transform.position) < distanceMax)
        {
            sweetspot = false;
            tooFar = false;
            tooClose = true;
        }
    }

    void Attack(Animator animator)
    {
        if (sweetspot)
        {
            animator.Play(animations[Random.Range(0, animations.Length)]);
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
