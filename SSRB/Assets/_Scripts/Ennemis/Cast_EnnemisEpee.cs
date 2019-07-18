﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cast_EnnemisEpee : StateMachineBehaviour
{
    public BoxCollider sword;
    EnnemisEppee_V2 idle;
    SwordCollision deflect;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        deflect = animator.GetComponentInChildren<SwordCollision>();
        SwordCollision.damage = 3;
        idle = animator.GetBehaviour<EnnemisEppee_V2>();
        idle.isBlocking = false;
        sword = animator.gameObject.GetComponentInChildren<BoxCollider>();
        sword.enabled = true;
    }

    
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (deflect.asBeenDeflected)
        {
            animator.Play("CastDeflect");
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
