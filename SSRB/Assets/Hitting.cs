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
        playerAttack.attackNumber = attackNumber;
        animator.ResetTrigger("Combo");
        
    }

   
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        if (attackNumber == 3)
        {
            Player.transform.position += animator.deltaPosition;
        }

        if (stateInfo.normalizedTime >= .2f)
        {
            playerAttack.canHit = true;
        }
    }

    
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.gameObject.GetComponent<PlayerCanHit>().canMove();
        animator.gameObject.GetComponent<PlayerCanHit>().cannontHit();

        if (attackNumber == 2)
        {
            playerAttack.attackNumber = 0;
        }
    }

    

    
}
