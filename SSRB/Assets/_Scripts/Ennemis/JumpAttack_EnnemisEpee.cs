using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JumpAttack_EnnemisEpee : StateMachineBehaviour
{
    EnnemisEppee_V2 idle;
    SwordCollision deflect;
    public int knockback;
    NavMeshAgent agent;
    Transform parent;
    GameObject player;
    public float attackSpeed;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<EnnemiEpee_CollisionDetector>().agent;
        deflect = animator.GetComponentInChildren<SwordCollision>();
        SwordCollision.damage = 4;
        SwordCollision.knockback = knockback;
        idle = animator.GetBehaviour<EnnemisEppee_V2>();
        agent.updateRotation = false;
        agent.speed = attackSpeed;
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (deflect.asBeenDeflected)
        {
            animator.Play("Impact");
            deflect.asBeenDeflected = false;
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        idle.asAttacked = true;
    }

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
