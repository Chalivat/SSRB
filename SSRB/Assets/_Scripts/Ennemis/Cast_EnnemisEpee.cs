using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast_EnnemisEpee : StateMachineBehaviour
{
    EnnemisEppee_V2 idle;
    SwordCollision deflect;
    DeflectImpact deflectImpact;
    public int knockback;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deflect = animator.GetComponentInChildren<SwordCollision>();
        deflectImpact = animator.GetBehaviour<DeflectImpact>();
        deflectImpact.isImpacted = true;
        SwordCollision.damage = 3;
        SwordCollision.knockback = knockback;
        idle = animator.GetBehaviour<EnnemisEppee_V2>();
        idle.isBlocking = false;
        //sword = GameObject.FindGameObjectWithTag("Sword").GetComponent<BoxCollider>();
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (deflect.asBeenDeflected)
        {
            animator.Play("Impact 0");
            deflect.asBeenDeflected = false;
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
