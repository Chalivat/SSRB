using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeflectImpact : StateMachineBehaviour
{
    public bool isImpacted = false;
    EnnemisEppee_V2 idle;
    EnnemiEpee_CollisionDetector detector;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        isImpacted = true;
        detector = animator.GetComponent<EnnemiEpee_CollisionDetector>();
        idle = animator.GetBehaviour<EnnemisEppee_V2>();
        idle.isBlocking = false;
        detector.isAgro = true;
        detector.isVulnerable = false;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Debug.Log("PUTAIN DE MERDE");
        detector.poise = 6;
    }



    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
