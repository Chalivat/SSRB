using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : StateMachineBehaviour
{
    public int attackNumber;
    private GameObject Player;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerAttack.StopMovement();
        
        if (attackNumber == 2)
        {
            animator.SetBool("wantToCombo",false);
        }
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (attackNumber == 2)
        {
            Player.transform.position += animator.deltaPosition;
        }

        if (attackNumber == 1 && stateInfo.normalizedTime >= stateInfo.length && animator.GetBool("wantToCombo"))
        {
            animator.SetBool("wantToCombo", false);
            animator.Play("SpinHit");
        }

        if (attackNumber == 1)
        {
            if (Input.GetButtonDown("Hit"))
            {
                animator.SetBool("wantToCombo", true);
            }
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    
}
