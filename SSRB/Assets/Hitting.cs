using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitting : StateMachineBehaviour
{
    public int attackNumber;
    private GameObject Player;
    private PlayerAttack playerAttack;
    private bool isHit;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        playerAttack = Player.GetComponent<PlayerAttack>();

        animator.gameObject.GetComponent<PlayerCanHit>().cannontHit();

        animator.SetBool("wantToCombo", false);

        playerAttack.attackNumber = attackNumber;
        animator.ResetTrigger("Combo");
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
        }

        if (playerAttack.wantToHit)
        {
            animator.SetBool("wantToCombo", true);
            //animator.Play("Idle");
        }
        else animator.SetBool("wantToCombo", false);
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerCanHit>().canMove();
        playerAttack.attackNumber = 0;
        animator.gameObject.GetComponent<PlayerCanHit>().cannontHit();
        
        if (playerAttack.wantToHit)
        {
            if (this.attackNumber == 0)
            {
                animator.Play("HitLeft");
            }
            else animator.Play("HIt");
            animator.SetBool("wantToCombo", true);
        }

        if (!playerAttack.wantToHit)
        {
            animator.Play("Idle");
        }
    }

    

    
}
