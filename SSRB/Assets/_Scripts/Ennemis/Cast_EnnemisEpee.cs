using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Cast_EnnemisEpee : StateMachineBehaviour
{
    EnnemisEppee_V2 idle;
    SwordCollision deflect;
    DeflectImpact deflectImpact;
    public int knockback;
    NavMeshAgent agent;
    public float attackSpeed;
    EnnemiEpee_CollisionDetector detector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent = animator.GetComponent<EnnemiEpee_CollisionDetector>().agent;
        detector = animator.GetComponent<EnnemiEpee_CollisionDetector>();
        agent.updateRotation = false;
        deflect = animator.GetComponentInChildren<SwordCollision>();
        deflectImpact = animator.GetBehaviour<DeflectImpact>();
        deflectImpact.isImpacted = false;
        SwordCollision.damage = 3;
        SwordCollision.knockback = knockback;
        idle = animator.GetBehaviour<EnnemisEppee_V2>();
        idle.isBlocking = false;
        agent.speed = attackSpeed;
        //sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (deflect.asBeenDeflected)
        {
            detector.poise -= Random.Range(5, 10);
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
