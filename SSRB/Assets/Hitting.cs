using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : StateMachineBehaviour
{
    public int attackNumber;
    private GameObject Player;
    private PlayerAttack playerAttack;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerAttack = Player.GetComponent<PlayerAttack>();

        animator.gameObject.GetComponent<PlayerCanHit>().cannontHit();
        
        if (attackNumber == 3)
        {
            animator.SetBool("wantToCombo",false);
        }
            playerAttack.attackNumber = attackNumber;
        
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (attackNumber == 3)
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
        animator.gameObject.GetComponent<PlayerCanHit>().canMove();
        playerAttack.attackNumber = 0;
        animator.gameObject.GetComponent<PlayerCanHit>().cannontHit();
    }

    

    
}
